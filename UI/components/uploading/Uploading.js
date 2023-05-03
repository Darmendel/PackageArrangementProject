import { useState } from 'react';
import './Uploading.css';
import Navbar from '../navbar/Navbar';
import { Link } from "react-router-dom";
import Papa from "papaparse";

// Allowed extensions for input file
const allowedExtensions = ["csv"];

const Uploading = () => {
  const [csvData, setCsvData] = useState([]);
  const [editableRowIndex, setEditableRowIndex] = useState(-1);
  const [newPackage, setNewPackage] = useState({
    UnpackOrder: '',
    Height: '',
    Width: '',
    Length: ''
  });

  function handleFileUpload(event) {
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.onload = (e) => {
      const parsedData = parseCSV(e.target.result);
      setCsvData(parsedData);
    };
    reader.readAsText(file);
  }

  function parseCSV(csvData) {
    const parsedData = Papa.parse(csvData, { header: true }).data;
    return parsedData;
  }

  function handleEditClick(rowIndex) {
    setEditableRowIndex(rowIndex);
  }

  function handleSaveClick(rowIndex) {
    setEditableRowIndex(-1);
  }

  function handleCancelClick() {
    setEditableRowIndex(-1);
  }

  function handleDeleteClick(rowIndex) {
    const newData = [...csvData];
    newData.splice(rowIndex, 1);
    setCsvData(newData);
  }

  return (
    <div className='uploading'>
      <div className='nav-upload'><Navbar /></div>
      <div className='table-wrapper'>
        {csvData.length === 0 &&
          <h1 htmlFor="csvInput" style={{ display: "block" }}>
            Enter CSV File: 
            <input className='input-csv' type="file" onChange={handleFileUpload} />
          </h1>
        }
        {csvData.length > 0 &&
          <div>
            
            <h2>Container size (CM):</h2>
            <form>
              <input className='input-container'
                type='number'
                name='height'
                required='required'
                placeholder='Enter height'
              />
              <input className='input-container'
                type='number'
                name='width'
                required='required'
                placeholder='Enter width'
              />
              <input className='input-container'
                type='number'
                name='length'
                required='required'
                placeholder='Enter length'
              />
              <button className='set-button' >Set</button>
            </form>

            <h2>Adding a new package:</h2>
            <form>
                <input className='input-add-package'
                type='number'
                name='unpackOrder'
                required='required'
                placeholder='Enter unpackOrder'
                value={newPackage.unpackOrder}
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
              <button className='add-button' onClick={(e) => {
                e.preventDefault();
                setCsvData([newPackage, ...csvData]);
                setNewPackage({
                  UnpackOrder: '',
                  Height: '',
                  Width: '',
                  Length: ''
                });
              }}>Add</button>
            </form>
            <table>
            <thead>
                <tr>
                  {Object.keys(csvData[0]).map((header, index) => (
                    <th key={index}>{header}</th>
                  ))}
                  <th className="actions">Actions</th>
                </tr>
              </thead>
              <tbody>
                {csvData
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
                                const newData = [...csvData];
                                newData[rowIndex][field] = e.target.value;
                                setCsvData(newData);
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
          </div>
        }
      </div>
    </div>
  );
};

export default Uploading;