import {React, useState} from "react";
import "./SignUp.css";
import { Link, useNavigate } from "react-router-dom";
import Navbar from "../navbar/Navbar";

const SignUp = () => {

  const useFormDataToServer = () => {
    const navigate = useNavigate();
  
    const sendFormDataToServer = async (formData) => {
      try {
        const response = await fetch('https://localhost:7165/api/User/SignUp', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          mode: "cors",
          body: JSON.stringify(formData),
        });
  
        if (response.ok) {
          console.log('FormData sent to the server successfully!');
          alert("The registration was successful!");
          navigate('/getStarted');
        } else {
          console.error('Failed to send FormData to the server.');
          alert("This email address is already in use.");
        }
      } catch (error) {
        console.error('Error while sending FormData to the server:', error);
      }
    };
  
    return sendFormDataToServer;
  };

  const [formData, setFormData] = useState({
    "email": "",
    "name": "",
    "password": ""
  });

  const handleChange = (event) => {
    setFormData({
      ...formData,
      [event.target.name]: event.target.value
    });
  };

  const sendFormDataToServer = useFormDataToServer(); 

  const handleSignClick = (event) => {
    event.preventDefault();
    sendFormDataToServer(formData);
  };

    
  return (
    <section id="signUp">
      <Navbar />
      <div className="container signUp" data-aos="fade-up">
        <h2>Please fill in the following details:</h2>
        <form>
          <div className="form-control-sign">
            Name:
            <input
              type="text"
              name="name"
              placeholder="Enter your first name..."
              value={formData.name}
              onChange={handleChange}
            />
            E-mail:
            <input
              type="email"
              name="email"
              placeholder="Enter your e-mail..."
              value={formData.email}
              onChange={handleChange}
            />
            Password:
            <input
              type="password"
              name="password"
              placeholder="Enter your password..."
              value={formData.password}
              onChange={handleChange}
            />
          </div>
          <button className="sign-lnk" onClick={handleSignClick}>Sign</button>
        </form>
        <div className="bottom-sign">
          <h1>Already have an account?</h1>
          <Link className="back-login" to="/getStarted">Login</Link>
        </div>
      </div>
    </section>
  );
};

export default SignUp;