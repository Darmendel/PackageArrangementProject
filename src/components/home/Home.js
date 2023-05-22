import React from 'react'
import Navbar from '../navbar/Navbar';
import Header from '../header/Header';
import Faq from '../faq/Faq';
import AboutUs from '../aboutUs/AboutUs';

const Home = () => {
  return (
    <div>
        <header className='header-bg'>
            <Navbar />
            <Header />
        </header>
        <Faq />
        <AboutUs />
    </div>
  )
}

export default Home
