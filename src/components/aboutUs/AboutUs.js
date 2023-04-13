import React, { useEffect } from "react";
import "./AboutUs.css";
import { GrCircleInformation } from "react-icons/gr";
import { Navbar } from "react-bootstrap";


const AboutUs = () => {
  return (
    <section id="aboutUs">
      <div className="container aboutUs" data-aos="fade-up">
        <div className="u-title" data-aos="fade-up">
          <GrCircleInformation color="orangered" size={30} />
          <h2>About Us</h2>
        </div>
        <h2>PackFriend is here to help you load cargo boxes and organize</h2>
        <h2>them efficiently, taking into account your customization.</h2>
        <h2 className="u-text-small">
          Simply provide us with your requirements, the rest is up to us.
        </h2>
      </div>
    </section>
  );
};

export default AboutUs;