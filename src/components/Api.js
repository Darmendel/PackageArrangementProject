async function sendDataToServer(packageData, containerData) {
  try {
    const response = await fetch('https://localhost:7165', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ packageData, containerData }),
    });

    if (response.ok) {
      console.log('Data sent to the server successfully!');
    } else {
      console.error('Failed to send data to the server.');
    }
  } catch (error) {
    console.error('Error while sending data to the server:', error);
  }
}

export { sendDataToServer };
