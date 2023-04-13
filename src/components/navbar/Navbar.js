import React, { useState } from "react";
import { AiOutlineBars } from "react-icons/ai";
import { RiCloseLine } from "react-icons/ri";
import { SiAnaconda } from "react-icons/si";
import Button from "../UI/Button/Button";
import "../UI/Button/Button.css";
import "./Navbar.css";

const Navbar = () => {
  const [showMenu, setShowMenu] = useState(false);

  const toggleMenu = () => {
    setShowMenu(!showMenu);
  };
  return (
    <nav className="navbar container">
      <div className="logo">
        <p className="logo-text">
          PACK<span>FRIEND</span>
        </p>
        <p className="logo-text-2">
          Your shipping FRIEND
        </p>
      </div>
      <menu>
        <ul
          className="nav-links"
          id={showMenu ? "nav-links-mobile" : "nav-links-mobile-hide"}
        >
          <li>
            <a href="/">Home</a>
          </li>
          <li>
            <a href="#language">Language</a>
          </li>
          <li>
            <a href="/#faq">FAQ</a>
          </li>
          <li>
            <a href="/#aboutUs">About Us</a>
          </li>
        </ul>
      </menu>
      <div className="menu-icons" onClick={toggleMenu}>
        {showMenu ? (
          <RiCloseLine color="#fff" size={30} />
        ) : (
          <AiOutlineBars color="#fff" size={27} />
        )}
      </div>
    </nav>
  );
};

export default Navbar;