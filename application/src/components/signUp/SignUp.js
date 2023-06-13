import {React, useState} from "react";
import "./SignUp.css";
import { Link, useNavigate } from "react-router-dom";
import Navbar from "../navbar/Navbar";
import { sendFormDataToServer } from '../Api';

const SignUp = () => {

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

  const navigate = useNavigate();

  const handleSignClick = (event) => {
    event.preventDefault();
    console.log(formData);
    sendFormDataToServer(formData);
    navigate('/getStarted');
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