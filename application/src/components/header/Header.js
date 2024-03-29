import React from "react";
import "./Header.css";
import "../Button/Button.css";
import { Link } from "react-router-dom";


const Header = () => {
  return (
    <section id="header">
      <div className="container header">
        <div className="header-left" data-aos="fade-right">
          <h1>
            <span>Package Organizer</span>
          </h1>
          <p className="u-text-small">
            Technology that revolutionized the organization
          </p>
          <div className="header-cta">
              <Link className="getStarted-lnk" to="/getStarted">Get Started</Link>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Header;