import {React, useState} from "react";
import "./GetStarted.css";
import { Link, useNavigate } from "react-router-dom";
import Navbar from "../navbar/Navbar";
import { sendLoginDataToServer } from '../Api';

const GetStarted = () => {
  
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

  const navigate = useNavigate();

  const handleLoginClick = (event) => {
    event.preventDefault();
    console.log(loginData);
    sendLoginDataToServer(loginData);
    navigate('/uploading');
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