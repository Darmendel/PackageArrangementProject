import React, {useState, useEffect, useRef} from "react";
import "./Container.css";
import { Link, useLocation, useNavigate } from "react-router-dom";
import Navbar from '../navbar/Navbar';

const Container = () => {

  const useDeliveryDataToServer = () => {
    const navigate = useNavigate();
  
    const sendDeliveryDataToServer = async (type, date, container, packages, userId) => {
      // console.log('check userid in sendDeliveryDataToServer:', userId);
      // console.log('date:', date);
      try {
        let url = null;
        if (type === 'fixed') {
          url = 'https://localhost:7165/api/User/' + userId + '/deliveries';
        } else {
          url = 'https://localhost:7165/api/User/' + userId + '/deliveries/custompackage';
        }
        // console.log('url:', url);
        console.log('body:', JSON.stringify({ date, packages, container }));
        const response = await fetch(url, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          mode: "cors",
          body: JSON.stringify({ date, packages, container }),
        });
  
        console.log('response:', response);
        const deliveryId = await response.text();
        console.log('deliveryId:', deliveryId);
  
        if (response.ok) {
          console.log('DeliveryData sent to the server successfully!');
        } else {
          console.error('Failed to send DeliveryData to the server.');
        }
      } catch (error) {
        console.error('Error while sending DeliveryData to the server:', error);
      }
    };
  
    return sendDeliveryDataToServer;
  }

  const location = useLocation();
  const [selectedContainer, setSelectedContainer] = useState("");
  const [customContainerValues, setCustomContainerValues] = useState({
    height: "",
    width: "",
    length: ""
  });
  const [packages, setPackages] = useState([]);
  const userIdRef = useRef(null); // Create a ref to store userId

  useEffect(() => {
    const searchParams = new URLSearchParams(location.search);
    const packagesParam = searchParams.get("packages");
    // console.log('packagesParam:', packagesParam);
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
          <h3>Height: 300 cm</h3>
          <h3>Width: 400 cm</h3>
          <h3>Length: 800 cm</h3>
        </div>
      );
      // <br></br>
      // <h2>Cost: 200 ₪</h2>
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
        </div>
      );
      // <br></br>
      // <h2>Cost: 400 ₪</h2>
    }
    return null;
  };

  const renderLargeContainerDetails = () => {
    if (selectedContainer === "large-container") {
      return (
        <div>
          <br></br>
          <h3>Height: 1200 cm</h3>
          <h3>Width: 1600 cm</h3>
          <h3>Length: 3200 cm</h3>
        </div>
      );
      // <br></br>
      // <h2>Cost: 800 ₪</h2>
    }
    return null;
  };

  const renderCustomContainerDetails = () => {
    if (selectedContainer === "custom-container") {
      return (
        <div>
          <form className="input-container">
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
          </form>
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
        console.log('deliveryDate, packages, containerSize:', JSON.stringify({ deliveryDate, packages, containerSize }));
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
            to="/visualization"
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
    </div>
  );
};

export default Container;


