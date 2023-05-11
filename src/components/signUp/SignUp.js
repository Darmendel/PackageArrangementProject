import {React, useEffect} from "react";
import "./SignUp.css";
import { Link } from "react-router-dom";
import Navbar from "../navbar/Navbar";

const SignUp = () => {
  
  const newUser = {
      firstName: 'Moria',
      lastName: 'Yefet',
      mail: 'moria.yefet@gmail.com',
      password: '123456'
  };

    const clickHandler = async () => {
      try {
        const res = await fetch('https://localhost:7165/api/User', {
          method: 'post',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(newUser)
        });
        const data = await res.json();

        if (!res.ok) {
          console.log(data.description);
          return;
        }

        console.log(data);
    } catch (error) {
        console.log(error);
      }
  };
  
  useEffect(() => {
    const btnEl = document.querySelector(".btn-submit-user");
    btnEl.addEventListener('click', clickHandler);
    return () => {
      btnEl.removeEventListener('click', clickHandler);
    };
  }, [clickHandler]);
  
  
  
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
          <button className="btn-submit-user">Submit User</button>
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