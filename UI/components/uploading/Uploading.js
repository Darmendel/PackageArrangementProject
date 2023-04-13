import { useState } from 'react'
import './Uploading.css'
import Navbar from '../navbar/Navbar';

import Papa from "papaparse";

// Allowed extensions for input file
const allowedExtensions = ["csv"];

const Uploading = () => {
  // const [file, setFile] = useState();
  // const [array, setArray] = useState([]);

  // const fileReader = new FileReader();

  // const handleOnChange = (e) => {
  //   setFile(e.target.files[0]);
  // };

  // const csvFileToArray = string => {
  //   const csvHeader = string.slice(0, string.indexOf("\n")).split(",");
  //   const csvRows = string.slice(string.indexOf("\n") + 1).split("\n");

  //   const array = csvRows.map(i => {
  //     const values = i.split(",");
  //     const obj = csvHeader.reduce((object, header, index) => {
  //       object[header] = values[index];
  //       return object;
  //     }, {});
  //     return obj;
  //   });

  //   setArray(array);
  // };

  // const handleOnSubmit = (e) => {
  //   e.preventDefault();

  //   if (file) {
  //     fileReader.onload = function (event) {
  //       const text = event.target.result;
  //       csvFileToArray(text);
  //     };

  //     fileReader.readAsText(file);
  //   }
  // };

  // const headerKeys = Object.keys(Object.assign({}, ...array));

  // This state will store the parsed data
  const [data, setData] = useState([]);

  // It state will contain the error when file is not used
  const [error, setError] = useState("");
   
  // It will store the file uploaded by the user
  const [file, setFile] = useState("");

  // This function will be called when the file input changes
  const handleFileChange = (e) => {
    setError("");
      
    // Check if user has entered the file
    if (e.target.files.length) {
      const inputFile = e.target.files[0];

      // Set the state
      setFile(inputFile);
    }
  };

  const handleParse = () => {
        
    // If user clicks the parse button without a file we show a error
    if (!file) return setError("Please upload a file first.");

    // Initialize a reader which allows user to read any file or blob.
    const reader = new FileReader();
      
    // Event listener on reader when the file loads, we parse it and set the data.
    reader.onload = async ({ target }) => {
      const csv = Papa.parse(target.result, { header: true });
      const parsedData = csv?.data;
      const columns = Object.keys(parsedData[0]);
      setData(columns);
    };
    reader.readAsText(file);
  };

  return (
    <div className='uploading' style={{ textAlign: "center" }}>
      <div className='nav-upload'><Navbar /></div>
      <h1 htmlFor="csvInput" style={{ display: "block" }}>
        Enter CSV File
      </h1>
        
        <input
          onChange={handleFileChange}
          id="csvInput"
          name="file"
          type="File"
          accept=".csv"
        />
        <button className='continue-upload' onClick={handleParse}>Continue</button>
          
      <div style={{ marginTop: "3rem" }}>
        {error ? error : data.map((col,
          idx) => <div key={idx}>{col}</div>)}
      </div>
    </div>
  //   <div className='uploading' style={{ textAlign: "center" }}>
  //     <div className='nav-upload'><Navbar /></div>
  //     <h1>Upload CSV File:</h1>
  //     <form>
  //       <input
  //       type={"file"}
  //       id={"csvFileInput"}
  //       accept={".csv"}
  //       onChange={handleOnChange}
  //       />
  //       <button className='continue-upload' type='submit'>Continue</button>
  //     </form>
  //     <br />
  //     <table>
  //       <thead>
  //       <tr key={"header"}>
  //           {headerKeys.map((key) => (
  //           <th>{key}</th>
  //           ))}
  //       </tr>
  //       </thead>

  //       <tbody>
  //       {array.map((item) => (
  //           <tr key={item.id}>
  //           {Object.values(item).map((val) => (
  //               <td>{val}</td>
  //           ))}
  //           </tr>
  //       ))}
  //       </tbody>
  //     </table>
  //   </div>
  );
};

export default Uploading;
