process.env['NODE_TLS_REJECT_UNAUTHORIZED'] = '0';

async function sendFormDataToServer() {
    const data = {
        "email": "2",
        "name": "2",
        "password": "2"
    };
    const response = await fetch('https://localhost:7165/api/User/SignUp', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    });

    console.log(response);
  
    if (response.ok) {
        console.log('data sent to the server successfully!');
    } else {
        console.error('Failed to send data to the server.');
    }
}
  
sendFormDataToServer();
  

