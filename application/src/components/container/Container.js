import React, {useState, useEffect, useRef} from "react";
import "./Container.css";
import { Link, useLocation, useNavigate } from "react-router-dom";
import Navbar from '../navbar/Navbar';

const BIG_NUMBER = 1000000;
const SMALL_NUMBER = 500;
const SECONDS = 5000;

const Container = () => {

  const userIdRef = useRef(null); // Create a ref to store userId
  const [userId, setUserId] = useState(null);
  const [deliveryId, setDeliveryId] = useState(null);
  const [deliveryStatus, setDeliveryStatus] = useState(null);
  const [intervalId, setIntervalId] = useState(null);

  const useDeliveryDataToServer = () => {
    const navigate = useNavigate();
  
    const sendDeliveryDataToServer = async (type, date, containerSize, packages, userId) => {
      try {
        setUserId(userId);
        let url = null;
        let request = null;
        if (type === 'fixed') {
          url = 'https://localhost:7165/api/User/' + userId + '/deliveries';
          request = {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            mode: "cors",
            body: JSON.stringify({ date, packages, containerSize }),
          }
        } else {
          url = 'https://localhost:7165/api/User/' + userId + '/deliveries/custompackage';
          let container = containerSize;
          request = {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            mode: "cors",
            body: JSON.stringify({ date, packages, container }),
          }
        }
        
        const response = await fetch(url, request);
  
        const deliveryId = await response.text();
        setDeliveryId(deliveryId);
  
        if (response.ok) {
          console.log('DeliveryData sent to the server successfully!');
          navigate('/waiting');
        } else {
          console.error('Failed to send DeliveryData to the server.');
        }
      } catch (error) {
        console.error('Error while sending DeliveryData to the server:', error);
      }
    };
  
    return sendDeliveryDataToServer;
  }

  const useDeliveryStatusFromServer = () => {

    const getDeliveryStatusFromServer = async () => {
      try {
        const url = 'https://localhost:7165/api/User/' + userId + '/deliveries/' + deliveryId + '/status';
        const response = await fetch(url, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
          mode: "cors",
        });
  
        if (response.ok) {
          console.log('DeliveryStatus accepted from the server successfully!');
          const deliveryStatus = await response.text();
          setDeliveryStatus(deliveryStatus);
          if (deliveryStatus === '2') {
            stopCheckingDeliveryStatus(); // Stop checking delivery status
            getDeliveryDataFromServer();
          }
        } else {
          console.error('Failed to accept DeliveryStatus from the server.');
        }
      } catch (error) {
        console.error('Error while accepting DeliveryStatus from the server:', error);
      }
    };

    const startCheckingDeliveryStatus = () => {
      // Start checking the delivery status every 5 seconds
      const intervalId = setInterval(() => {
        getDeliveryStatusFromServer();
      }, SECONDS);
      setIntervalId(intervalId);
    };
  
    const stopCheckingDeliveryStatus = () => {
      // Clear the interval
      if (intervalId) {
        clearInterval(intervalId);
        setIntervalId(null);
      }
    };
  
    return { startCheckingDeliveryStatus, stopCheckingDeliveryStatus };
  }

  const useDeliveryDataFromServer = () => {

    const [deliveryData, setDeliveryData] = useState(null);
    const navigate = useNavigate();
  
    const getDeliveryDataFromServer = async () => {
      try {
        let url = 'https://localhost:7165/api/User/' + userId + '/deliveries/' + deliveryId;
        const response = await fetch(url, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
          mode: "cors",
        });

        const deliveryData = await response.text();
  
        if (response.ok) {
          setDeliveryData(deliveryData);
          navigate('/visualization', {"state": deliveryData});
        } else {
          console.error('Failed to accept DeliveryData from the server.');
        }
      } catch (error) {
        console.error('Error while accepting DeliveryData from the server:', error);
      }
    };
  
    return { getDeliveryDataFromServer, deliveryData };
  }

  const { startCheckingDeliveryStatus, stopCheckingDeliveryStatus } = useDeliveryStatusFromServer();
  const { getDeliveryDataFromServer } = useDeliveryDataFromServer();
  
  useEffect(() => {
    // Start checking the delivery status when the component mounts
    startCheckingDeliveryStatus(userIdRef.current, deliveryId);
  
    // Clean up the interval when the component unmounts
    return () => {
      stopCheckingDeliveryStatus();
    };
  }, [startCheckingDeliveryStatus, stopCheckingDeliveryStatus, userIdRef, deliveryId]);
  
  const location = useLocation();
  const [selectedContainer, setSelectedContainer] = useState("");
  const [customContainerValues, setCustomContainerValues] = useState({
    height: "",
    width: "",
    length: ""
  });
  const [packages, setPackages] = useState([]);
  
  useEffect(() => {
    const searchParams = new URLSearchParams(location.search);
    const packagesParam = searchParams.get("packages");
    const userId = searchParams.get("userId");
    if (packagesParam) {
      const parsedCsvData = JSON.parse(decodeURIComponent(packagesParam));
      setPackages(parsedCsvData);
    }
    userIdRef.current = userId; // Assign the value to the ref
  }, [location.search]);

  const handleContainerClick = (containerId) => {
    setSelectedContainer(containerId);
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setCustomContainerValues((prevValues) => ({
      ...prevValues,
      [name]: value
    }));
  };

  const isContainerSelected = (containerId) => {
    return selectedContainer === containerId;
  };

  const isCustomContainerFilled = () => {
    const { height, width, length } = customContainerValues;
    return height !== "" && width !== "" && length !== "";
  };

  const renderSmallContainerDetails = () => {
    if (selectedContainer === "small-container") {
      return (
        <div>
          <br></br>
          <h3>Height: 400 cm</h3>
          <h3>Width: 600 cm</h3>
          <h3>Length: 1400 cm</h3>
          <br></br>
          <h2>Cost: 700 ₪</h2>
        </div>
      );
    }
    return null;
  };

  const renderMediumContainerDetails = () => {
    if (selectedContainer === "medium-container") {
      return (
        <div>
          <br></br>
          <h3>Height: 600 cm</h3>
          <h3>Width: 800 cm</h3>
          <h3>Length: 1600 cm</h3>
          <br></br>
          <h2>Cost: 850 ₪</h2>
        </div>
      );
    }
    return null;
  };

  const renderLargeContainerDetails = () => {
    if (selectedContainer === "large-container") {
      return (
        <div>
          <br></br>
          <h3>Height: 800 cm</h3>
          <h3>Width: 1000 cm</h3>
          <h3>Length: 1800 cm</h3>
          <br></br>
          <h2>Cost: 1000 ₪</h2>
        </div>
      );
    }
    return null;
  };

  const renderCustomContainerDetails = () => {
    if (selectedContainer === "custom-container") {
      const { height, width, length } = customContainerValues;
      // Calculate the cost based on the values of the container
      var cost = height * width * length / BIG_NUMBER + SMALL_NUMBER;
      return (
        <div className="input-container">
          <input 
            type="number" 
            required="required" 
            placeholder="Enter height"
            name="height"
            value={customContainerValues.height}
            onChange={handleInputChange}
          />
          <input 
            type="number" 
            required="required" 
            placeholder="Enter width"
            name="width"
            value={customContainerValues.width}
            onChange={handleInputChange}
          />
          <input 
            type="number" 
            required="required" 
            placeholder="Enter length"
            name="length"
            value={customContainerValues.length}
            onChange={handleInputChange}
          />
          <br></br>
          <h2>Cost: <h6>(Depends on your choice)</h6> {cost} ₪</h2>
        </div>
      );
    }
    return null;
  };

  const sendDeliveryDataToServer = useDeliveryDataToServer();

  const renderContinueButton = () => {
    let isClickable = false;

    if (selectedContainer === "custom-container") {
      isClickable = isCustomContainerFilled();
    } else {
      isClickable = selectedContainer !== "";
    }

    const handleContinueClick = () => {
      const currentDate = new Date(); // Get the current date and time
      const deliveryDate = currentDate.toISOString(); // Convert the date to a string format
      let containerData = {};
      let containerSize = 0;
      let type = null;
      if (selectedContainer === 'custom-container') {
        containerData = customContainerValues;
        type = 'custom';
        sendDeliveryDataToServer(type, deliveryDate, containerData, packages, userIdRef.current);
      } else if (selectedContainer === 'small-container') {
        containerSize = 1;
        type = 'fixed';
        sendDeliveryDataToServer(type, deliveryDate, containerSize, packages, userIdRef.current);
      } else if (selectedContainer === 'medium-container') {
        containerSize = 2;
        type = 'fixed';
        sendDeliveryDataToServer(type, deliveryDate, containerSize, packages, userIdRef.current);
      } else if (selectedContainer === 'large-container') {
        containerSize = 3;
        type = 'fixed';
        sendDeliveryDataToServer(type, deliveryDate, containerSize, packages, userIdRef.current);
      }
    };

    if (isClickable) {
      return (
        <div>
          <Link 
            className={"continue-container-clickable"}
            disabled={false} 
            // to="/waiting"
            onClick={handleContinueClick} 
          >
            Continue
          </Link>
        </div>
      );
    } else {
      return (
        <div>
          <Link 
            className={"continue-container"}
            disabled={true} 
            to="/container"
          >
            Continue
          </Link>
        </div>
      );
    }
  };



  return (
    <div>
      <div className="nav-upload">
        <Navbar />
      </div>
      <div className="container-wrapper">
        <div 
            id="small-container"
            className={isContainerSelected("small-container") ? "selected-container" : ""} 
            onClick={() => handleContainerClick("small-container")}
          >
          <input type="radio" id="small" name="container-size" />
          <label htmlFor="small">
            <img
              id="radio-img-small"
              src="https://www.saloodo.com/wp-content/uploads/2021/09/container-1-1.png"
              alt="Small Container"
            />
            <h1>Small</h1>
          </label>
          {renderSmallContainerDetails()}
        </div>
        <div 
            id="medium-container"
            className={isContainerSelected("medium-container") ? "selected-container" : ""} 
            onClick={() => handleContainerClick("medium-container")}
          >
          <input type="radio" id="medium" name="container-size" />
          <label htmlFor="medium">
            <img
              id="radio-img-medium"
              src="https://www.saloodo.com/wp-content/uploads/2021/09/container-1-1.png"
              alt="Medium Container"
            />
            <h1>Medium</h1>
          </label>
          {renderMediumContainerDetails()}
        </div>
        <div 
            id="large-container"
            className={isContainerSelected("large-container") ? "selected-container" : ""} 
            onClick={() => handleContainerClick("large-container")}
          >
          <input type="radio" id="large" name="container-size" />
          <label htmlFor="large">
            <img
              id="radio-img-large"
              src="https://www.saloodo.com/wp-content/uploads/2021/09/container-1-1.png"
              alt="Large Container"
            />
            <h1>Large</h1>
          </label>
          {renderLargeContainerDetails()}
        </div>
        <div 
            id="custom-container" 
            className={isContainerSelected("custom-container") ? "selected-container" : ""}
            onClick={() => handleContainerClick("custom-container")}
          >
          <input type="radio" id="custom" name="container-size" />
          <label htmlFor="custom">
            <h1>Custom</h1>
          </label>
          {renderCustomContainerDetails()}
        </div>
      </div>
      {renderContinueButton()}
      {deliveryStatus}
    </div>
  );
};

export default Container;