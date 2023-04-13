import './App.css';
import Home from './components/home/Home';
import GetStarted from './components/getStarted/GetStarted';
import { Link, BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import SignUp from './components/signUp/SignUp';
import Uploading from './components/uploading/Uploading';
import React, { useState } from 'react';


function App() {
  
  return (
    <Router>
      <Routes>
        <Route path='/' element={<Home />}></Route>
        <Route path='/getStarted' element={<GetStarted />}></Route>
        <Route path='/signUp' element={<SignUp />}></Route>
        <Route path='/uploading' element={<Uploading />}></Route>
      </Routes>
    </Router>
  );
}

export default App;