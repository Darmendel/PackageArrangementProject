import './App.css';
import Home from './components/home/Home';
import GetStarted from './components/getStarted/GetStarted';
import {BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import SignUp from './components/signUp/SignUp';
import Uploading from './components/uploading/Uploading';
import Container from './components/container/Container';
import Visualization from './components/visualization/Visualization';
import {React, useState } from 'react';


function App() {
  
  const [userId, setUserId] = useState(null);

  const handleLoginSuccess = (userId) => {
    setUserId(userId);
  };

  return (
    <Router>
      <Routes>
        <Route path='/' element={<Home />}></Route>
        <Route path='/getStarted' element={<GetStarted handleLoginSuccess={handleLoginSuccess} />}></Route>
        <Route path='/signUp' element={<SignUp />}></Route>
        <Route path='/uploading' element={<Uploading userId={userId} />}></Route>
        <Route path='/container' element={<Container />}></Route>
        <Route path='/visualization' element={<Visualization />}></Route>
      </Routes>
    </Router>
  );
}

export default App;
