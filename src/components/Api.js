async function sendDeliveryDataToServer(containerData, packageData) {
  try {
    const response = await fetch('https://localhost:7165', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ containerData, packageData }),
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

async function sendFormDataToServer(formData) {
  try {
    const response = await fetch('https://localhost:7165', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ formData }),
    });

    if (response.ok) {
      console.log('UserData sent to the server successfully!');
    } else {
      console.error('Failed to send UserData to the server.');
    }
  } catch (error) {
    console.error('Error while sending UserData to the server:', error);
  }
}

async function sendLoginDataToServer(loginData) {
  try {
    const response = await fetch('https://localhost:7165', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ loginData }),
    });

    if (response.ok) {
      console.log('LoginData sent to the server successfully!');
    } else {
      console.error('Failed to send LoginData to the server.');
    }
  } catch (error) {
    console.error('Error while sending LoginData to the server:', error);
  }
}

export { sendDeliveryDataToServer, sendFormDataToServer, sendLoginDataToServer };
