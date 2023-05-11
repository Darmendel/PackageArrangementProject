import React from "react";
import "./Container.css";
import { Link } from "react-router-dom";
import Navbar from '../navbar/Navbar';


const Container = () => {
  
    const radioBtn = document.getElementById('radio-btn');
    const radioImg = document.getElementById('radio-img');

    document.addEventListener('DOMContentLoaded', function() {
    if (radioBtn.checked) {
        radioImg.src = "./https://www.saloodo.com/wp-content/uploads/2021/09/container-1-1.png";
    } else {
        radioImg.src = "https://www.saloodo.com/wp-content/uploads/2021/09/container-1-1.png";
    }
    });



  
  
    return (
    <div>
        <div className='nav-upload'><Navbar /></div>
        <input type="radio" id="radio-btn"/>
        <label for="radio-btn">
            <img id="radio-img-small" src="https://www.saloodo.com/wp-content/uploads/2021/09/container-1-1.png"/>
            <img id="radio-img-medium" src="https://www.saloodo.com/wp-content/uploads/2021/09/container-1-1.png"/>
            <img id="radio-img-large" src="https://www.saloodo.com/wp-content/uploads/2021/09/container-1-1.png"/>
            <label id="radio-custom">Custom</label>
            <h2>Container size (CM):</h2>
            <form>
              <input className='input-container'
                type='number'
                name='height'
                required='required'
                placeholder='Enter height'
              />
              <input className='input-container'
                type='number'
                name='width'
                required='required'
                placeholder='Enter width'
              />
              <input className='input-container'
                type='number'
                name='length'
                required='required'
                placeholder='Enter length'
              />
              <button className='set-button' >Set</button>
            </form>
        </label>
    </div>
  );
};

export default Container;