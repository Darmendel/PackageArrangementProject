import {React, useEffect} from "react";
import "./SignUp.css";
import { Link } from "react-router-dom";
import Navbar from "../navbar/Navbar";

const SignUp = () => {

  return (
    <section id="signUp">
      <Navbar />
      <div className="container signUp" data-aos="fade-up">
        <h2>Please fill in the following details:</h2>
        <form>
          <div className="form-control-sign">
            First name:
            <input type="text" placeholder="Enter your first name..." />
            Last name:
            <input type="taxt" placeholder="Enter your last name..." />
            ID number:
            <input type="number" placeholder="Enter your ID number..." />
            E-mail:
            <input type="email" placeholder="Enter your e-mail..." />
            Password:
            <input type="password" placeholder="Enter your password..." />
          </div>
          <Link className="sign-lnk" to="/uploading">Sign</Link>
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