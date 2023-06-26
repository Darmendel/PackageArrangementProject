process.env['NODE_TLS_REJECT_UNAUTHORIZED'] = '0';

async function sendFormDataToServer(formData) {
  try {
    const response = await fetch('https://localhost:7165/api/User/SignUp', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      mode: "cors",
      body: JSON.stringify(formData),
    });

    console.log('response:', response);
  
    if (response.ok) {
      console.log('FormData sent to the server successfully!');
    } else {
      console.error('Failed to send FormData to the server.');
    }
  } catch (error) {
    console.error('Error while sending FormData to the server:', error);
  }
}

async function sendLoginDataToServer(loginData) {
  try {
    const response = await fetch('https://localhost:7165/api/User/Login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      mode: "cors",
      body: JSON.stringify(loginData),
    });

    console.log('response:', response);

    if (response.ok) {
      console.log('LoginData sent to the server successfully!');
    } else {
      console.error('Failed to send LoginData to the server.');
    }
  } catch (error) {
    console.error('Error while sending LoginData to the server:', error);
  }
}

async function sendDeliveryDataToServer(url, container, packageData) {
  try {
    const response = await fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ container, packageData }),
    });

    if (response.ok) {
      console.log('DeliveryData sent to the server successfully!');
    } else {
      console.error('Failed to send DeliveryData to the server.');
    }
  } catch (error) {
    console.error('Error while sending DeliveryData to the server:', error);
  }
}

export { sendDeliveryDataToServer, sendFormDataToServer, sendLoginDataToServer };
