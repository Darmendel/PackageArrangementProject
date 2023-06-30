import { useState, useRef } from 'react';
import './Uploading.css';
import Navbar from '../navbar/Navbar';
import { Link } from "react-router-dom";
import Papa from "papaparse";


const Uploading = ({ userId }) => {
  const tableRef = useRef(null); // Reference to the table element
  const [packages, setPackages] = useState([]);
  const [editableRowIndex, setEditableRowIndex] = useState(-1);
  const [newPackage, setNewPackage] = useState({
    order: '', 
    height: '', 
    width: '', 
    length: ''
  });  

  function handleFileUpload(event) {
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.onload = (e) => {
      const parsedData = parseCSV(e.target.result);
      setPackages(parsedData);
    };
    reader.readAsText(file);
  }

  function parseCSV(packages) {
    // Split the packages string into an array of lines
    const lines = packages.split('\n');
    // Remove carriage returns from each line
    const cleanedLines = lines.map(line => line.replace('\r', ''));
    // Remove empty lines at the beginning or end of the array
    while (cleanedLines.length > 0 && cleanedLines[0].trim() === '') {
      cleanedLines.shift();
    }
    
    while (cleanedLines.length > 0 && cleanedLines[cleanedLines.length - 1].trim() === '') {
      cleanedLines.pop();
    }
    // Join the lines back into a string
    const cleanedPackages = cleanedLines.join('\n');
    // Parse the cleaned packages data
    const parsedData = Papa.parse(cleanedPackages, { header: true }).data;
    return parsedData;
  }

  function handleEditClick(rowIndex) {
    setEditableRowIndex(rowIndex);
  }

  function handleSaveClick() {
    setEditableRowIndex(-1);
  }

  function handleCancelClick() {
    setEditableRowIndex(-1);
  }

  function handleDeleteClick(rowIndex) {
    const newData = [...packages];
    newData.splice(rowIndex, 1);
    setPackages(newData);
  }

  function handleAddClick(e) {
    e.preventDefault();
    const { order, height, width, length } = newPackage;
    const newPackageObject = {
      order, 
      height, 
      width, 
      length 
    };
    const newData = [...packages, newPackageObject];
    setPackages(newData);
    setNewPackage({
      order: '', 
      height: '', 
      width: '', 
      length: '' 
    });
  
    // Scroll to the last row in the table
    if (tableRef.current) {
      const table = tableRef.current;
      const rows = table.getElementsByTagName('tr');
      const lastRow = rows[rows.length - 1];
      lastRow.scrollIntoView({ behavior: 'smooth' });
    }
  }

  function convertPackages(packages) {
    return packages.map(({ "Unpack order": order, Height, Width, Length }) => ({
      order: order,
      height: Height,
      width: Width,
      length: Length
    }));
  }

  return (
    <div className='uploading'>
      <div className='nav-upload'><Navbar /></div>
      <div className='table-wrapper'>
        {packages.length === 0 &&
          <h1 htmlFor="csvInput" style={{ display: "block" }}>
            Enter CSV File: 
            <input className='input-csv' type="file" accept='.csv' onChange={handleFileUpload} />
          </h1>
        }
        {packages.length > 0 &&
          <div>
            <h2>Adding a new package:</h2>
            <form className='add-line'>
                <input className='input-add-package'
                type='number'
                name='unpackOrder'
                required='required'
                placeholder='Enter unpackOrder'
                value={newPackage.UnpackOrder}
                onChange={(e) =>
                  setNewPackage({ ...newPackage, UnpackOrder: e.target.value })
                }
              />
              <input className='input-add-package'
                type='number'
                name='height'
                required='required'
                placeholder='Enter height'
                value={newPackage.Height}
                onChange={(e) =>
                  setNewPackage({ ...newPackage, Height: e.target.value })
                }
              />
              <input className='input-add-package'
                type='number'
                name='width'
                required='required'
                placeholder='Enter width'
                value={newPackage.Width}
                onChange={(e) =>
                  setNewPackage({ ...newPackage, Width: e.target.value })
                }
              />
              <input className='input-add-package'
                type='number'
                name='length'
                required='required'
                placeholder='Enter length'
                value={newPackage.Length}
                onChange={(e) =>
                  setNewPackage({ ...newPackage, Length: e.target.value })
                }
              />
              <button className='add-button' onClick={handleAddClick}>Add</button>
              <Link 
                className="continue-lnk-top" 
                // pass the packages as a query parameter in the URL when navigating to 
                // the Container page
                to={{ pathname: "/container", 
                search: `?packages=${encodeURIComponent(JSON.stringify(convertPackages(packages)))}&userId=${encodeURIComponent(userId)}`}}>
                Continue
              </Link>
            </form>
            
            <table ref={tableRef}>
              <thead>
                <tr>
                  {Object.keys(packages[0]).map((header, index) => (
                    <th key={index}>{header}</th>
                  ))}
                  <th className="actions">Actions</th>
                </tr>
              </thead>
              <tbody>
                {packages
                  .filter((row) => Object.values(row).some((value) => value !== ''))
                  .map((row, rowIndex) => (
                    <tr key={rowIndex}>
                      {Object.entries(row).map(([field, value], cellIndex) => (
                        <td key={cellIndex}>
                          {editableRowIndex === rowIndex ? (
                            <input
                              className="input-body"
                              type="text"
                              defaultValue={value}
                              onBlur={(e) => {
                                const newData = [...packages];
                                newData[rowIndex][field] = e.target.value;
                                setPackages(newData);
                              }}
                              style={{ color: "black" }}
                            />
                          ) : (
                            value
                          )}
                        </td>
                      ))}
                      <td className="actions">
                        {editableRowIndex === rowIndex ? (
                          <div>
                            <button onClick={() => handleSaveClick(rowIndex)}>Save</button>
                            <button onClick={handleCancelClick}>Cancel</button>
                          </div>
                        ) : (
                          <div>
                            <button onClick={() => handleEditClick(rowIndex)}>Edit</button>
                            <button
                              onClick={() => handleDeleteClick(rowIndex)}
                              disabled={Object.values(row).every((value) => value === '')}
                            >
                              Delete
                            </button>
                          </div>
                        )}
                      </td>
                    </tr>
                  ))}            
              </tbody>
            </table>
            <Link 
            className="continue-lnk-bottom" 
            // pass the packages as a query parameter in the URL when navigating to 
            // the Container page
            to={{ pathname: "/container", 
            search: `?packages=${encodeURIComponent(JSON.stringify(convertPackages(packages)))}&userId=${encodeURIComponent(userId)}`}}>
            Continue
          </Link>
          </div>
        }
      </div>
    </div>
  );
};

export default Uploading;