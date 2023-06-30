import {React, useState} from "react";
import "./GetStarted.css";
import { Link, useNavigate } from "react-router-dom";
import Navbar from "../navbar/Navbar";

const GetStarted = ({ handleLoginSuccess }) => {

  const useLoginDataToServer = () => {
    const navigate = useNavigate();
    const [userId, setUserId] = useState(null);
  
    const sendLoginDataToServer = async (loginData) => {
      try {
        const response = await fetch('https://localhost:7165/api/User/Login', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          mode: "cors",
          body: JSON.stringify(loginData),
        });
  
        const userId = await response.text();
  
        console.log('response:', response);
  
        if (response.ok) {
          setUserId(userId);
          navigate('/uploading');
          handleLoginSuccess(userId);
        } else {
          console.error('Failed to send LoginData to the server.');
          alert("One or more of the details you entered are incorrect. Please try again.");
        }
      } catch (error) {
        console.error('Error while sending LoginData to the server:', error);
      }
    };
  
    return {sendLoginDataToServer, userId};
  }
  
  const [loginData, setLoginData] = useState({
    "email": "",
    "password": ""
  });

  const handleChange = (event) => {
    setLoginData({
      ...loginData,
      [event.target.name]: event.target.value
    });
  };

  const { sendLoginDataToServer } = useLoginDataToServer();

  const handleLoginClick = (event) => {
    event.preventDefault();
    console.log(loginData);
    sendLoginDataToServer(loginData);
  };
  
  return (
    <section id="getStarted">
      <Navbar />
      <div className="container getStarted" data-aos="fade-up">
        <h2>Login to your account</h2>
        <form>
          <div className="form-control">
            <input 
              type="email" 
              name="email" 
              placeholder="Enter your e-mail..."
              value={loginData.email}
              onChange={handleChange}
            />
            <input 
              type="password" 
              name="password" 
              placeholder="Enter your password..." 
              onChange={handleChange}
            />
          </div>
          <button className="login-lnk" onClick={handleLoginClick}>Login</button>
        </form>
        <div className="bottom">
          <h1>Don't have an account yet?</h1>
          <Link className="signUp-lnk" to="/signUp">Sign up now</Link>
        </div>
      </div>
    </section>
  );
};

export default GetStarted;