import React from "react";


const Request = () => {

    const btnEl = document.querySelector('.btn');
    const newUser = {
        firstName: 'Moria',
        lastName: 'Yefet',
        mail: 'moria.yefet@gmail.com',
        password: '123456'
    };

    const clickHandler = async () => {
        try {
            const res = await fetch('https://localhost:7165', {
                method: 'Post',
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

    return (
        <div></div>
    );
};

export default Request;




