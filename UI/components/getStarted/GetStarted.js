import React from "react";
import "./GetStarted.css";
import { Link } from "react-router-dom";
import Navbar from "../navbar/Navbar";
import SignUp from "../signUp/SignUp";

const GetStarted = () => {
  return (
    <section id="getStarted">
      <Navbar />
      <div className="container getStarted" data-aos="fade-up">
        <h2>Login to your account</h2>
        <form>
          <div className="form-control">
            <input type="text" placeholder="Enter your name..." />
            <input type="password" placeholder="Enter your password..." />
          </div>
          <Link className="login-lnk" to="/uploading">Login</Link>
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