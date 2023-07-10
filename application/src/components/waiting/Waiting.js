import React from "react";
import "./Waiting.css";
import Navbar from "../navbar/Navbar";
import { library } from "@fortawesome/fontawesome-svg-core";
import { faSpinner } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

// Add the spinner icon to the Font Awesome library
library.add(faSpinner);

const Waiting = () => {
  return (
    <section id="waiting">
      <Navbar />
      <div className="waiting-content">
        <h1>Please wait while the system arranges the boxes for you...</h1>
        <div className="spinner-container">
        <FontAwesomeIcon icon={faSpinner} spin size="4x" />
        </div>
      </div>
    </section>
  );
};

export default Waiting;
