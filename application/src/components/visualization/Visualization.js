import React, { useEffect, useRef } from 'react';
import './Visualization.css';
import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls';
import { useLocation } from 'react-router-dom';

const SCALE = 100;

const Visualization = () => {
  const {state} = useLocation();
  // console.log('deliveryData in beginning:', state);
  const ref = useRef(null);
  const selectedBox = useRef(null);
  const originalPos = useRef(null);

  const data = JSON.parse(state);

  // Sort the packages by ID
  data.firstPackages.sort((a, b) => a.id - b.id);
  data.secondPackages.sort((a, b) => a.id - b.id);

  // var selectedBox = null;

  const cancelBoxSelection = () => {
    // Reset the box's position to its original position
    if (originalPos.current) {
      selectedBox.current.position.copy(originalPos.current);
    }
    const cancelButton = document.getElementById("cancel-button");
    if (cancelButton) {
      cancelButton.remove(); // Remove the cancel button from the DOM
    }
    selectedBox.current = null; // Deselect the box
  };

  const renderCancelButton = () => {
    if (selectedBox.current) {
      var cancelButton = document.getElementById("cancel-button");
      if (!cancelButton) {
        // Create the cancel button and append it to the cancel container
        const cancelContainer = document.createElement("div");
        cancelContainer.id = "cancel-container";
        cancelContainer.style.position = "absolute";
        cancelContainer.style.top = "50px";
        cancelContainer.style.left = "250px";
        ref.current.appendChild(cancelContainer);

        cancelButton = document.createElement("button");
        cancelButton.id = "cancel-button";
        cancelButton.innerHTML = "Deselect box";
        cancelButton.style.color = "orangered";
        cancelButton.style.backgroundColor = "white";
        cancelButton.style.borderRadius = "5px 5px 5px 5px";
        cancelButton.style.padding = "10px";
        cancelButton.addEventListener("click", () => cancelBoxSelection());
        cancelContainer.appendChild(cancelButton);
      }
    }
  };

  useEffect(() => {
    var solution1Visible = false;
    var solution2Visible = false;
    var list1Visible = false;
    var list2Visible = false;

    // first solution:

    const boxesWidthFirst = [];
    const boxesHeightFirst = [];
    const boxesDepthFirst = [];

    data.firstPackages.forEach(function(box) {
      boxesWidthFirst.push(box.width / SCALE);
      boxesHeightFirst.push(box.height / SCALE);
      boxesDepthFirst.push(box.length / SCALE);
    });

    const boxesIdFirst = [];

    data.firstPackages.forEach(function(box) {
      boxesIdFirst.push(box.id);
    })

    const boxesOrderFirst = [];

    data.firstPackages.forEach(function(box) {
      boxesOrderFirst.push(box.order);
    })

    const xPositionsFirst = [];
    const yPositionsFirst = [];
    const zPositionsFirst = [];

    // save the values before scaling by 100
    const xPositionsFirstOriginal = [];
    const yPositionsFirstOriginal = [];
    const zPositionsFirstOriginal = [];

    data.firstPackages.forEach(function(box) {
      zPositionsFirstOriginal.push(box.x);
      xPositionsFirstOriginal.push(box.y);
      yPositionsFirstOriginal.push(box.z);
      if (box.x !== 0) {
        box.x /= SCALE;
      }
      zPositionsFirst.push(box.x);
      if (box.y !== 0) {
        box.y /= SCALE;
      }
      xPositionsFirst.push(box.y);
      if (box.z !== 0) {
        box.z /= SCALE;
      }
      yPositionsFirst.push(box.z);
    });

    // second solution:

    const boxesWidthSecond = [];
    const boxesHeightSecond = [];
    const boxesDepthSecond = [];

    data.secondPackages.forEach(function(box) {
      boxesWidthSecond.push(box.width / SCALE);
      boxesHeightSecond.push(box.height / SCALE);
      boxesDepthSecond.push(box.length / SCALE);
    });

    const boxesIdSecond = [];

    data.secondPackages.forEach(function(box) {
      boxesIdSecond.push(box.id);
    })

    const boxesOrderSecond = [];

    data.secondPackages.forEach(function(box) {
      boxesOrderSecond.push(box.order);
    })

    const xPositionsSecond = [];
    const yPositionsSecond = [];
    const zPositionsSecond = [];

    // save the values before scaling by 100
    const xPositionsSecondOriginal = [];
    const yPositionsSecondOriginal = [];
    const zPositionsSecondOriginal = [];

    data.secondPackages.forEach(function(box) {
      zPositionsSecondOriginal.push(box.x);
      xPositionsSecondOriginal.push(box.y);
      yPositionsSecondOriginal.push(box.z);
      if (box.x !== 0) {
        box.x /= SCALE;
      }
      zPositionsSecond.push(box.x);
      if (box.y !== 0) {
        box.y /= SCALE;
      }
      xPositionsSecond.push(box.y);
      if (box.z !== 0) {
        box.z /= SCALE;
      }
      yPositionsSecond.push(box.z);
    });
    
    var numOfBoxes1 = xPositionsFirst.length;
    var numOfBoxes2 = xPositionsSecond.length;
    const containerHeight = data.container.height / SCALE;
      
    var boxes = [];
    var isGround = false;

    var boxWidth = 0;
    var boxHeight = 0;
    var boxDepth = 0;

    var ground = null;
    const groundWidth = data.container.width / SCALE;
    const groundHeight = 0;
    const groundDepth = data.container.length / SCALE;
  
    var scene = new THREE.Scene();
    scene.background = new THREE.Color(0xa1930); 
    var camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);

    var raycaster = new THREE.Raycaster();
    var mouse = new THREE.Vector2();
    var renderer = new THREE.WebGLRenderer();

    renderer.shadowMap.enabled = true;
    renderer.setSize(window.innerWidth, window.innerHeight);
    ref.current.appendChild(renderer.domElement);

    var controls = new OrbitControls(camera, renderer.domElement);
    controls.enabledPan = false;
    controls.maxPolarAngle = Math.PI / 2;

    const color = getRandomColor();
    var originalBoxMaterial = new THREE.MeshStandardMaterial({ color: color });

    class Box extends THREE.Mesh {
      constructor({ Id, width, height, depth, position, originalPosition, order }) {
        const color = getRandomColor();
        originalBoxMaterial = new THREE.MeshStandardMaterial({ color: color });

        super(new THREE.BoxGeometry(width, height, depth), originalBoxMaterial);
        
        this.Id = Id;
        this.order = order;
        this.originalPosition = originalPosition;

        this.height = height;
        this.width = width;
        this.depth = depth;
        
        this.position.copy(position);
        
        this.bottom = this.position.y - this.height / 2;
        this.top = this.position.y + this.height / 2;
        
        this.right = this.position.x + this.width / 2;
        this.left = this.position.x - this.width / 2;

        this.front = this.position.z - this.depth / 2;
        this.back = this.position.z + this.depth / 2;
      }

      updateSides() {
        this.bottom = this.position.y - this.height / 2;
        this.top = this.position.y + this.height / 2;

        this.right = this.position.x + this.width / 2;
        this.left = this.position.x - this.width / 2;

        this.front = this.position.z - this.depth / 2;
        this.back = this.position.z + this.depth / 2;
      }
      
      update() {
        this.updateSides();
      }
    }



    const handleExportSolution = () => {
      // Combine the data from firstPackages and secondPackages
      const combinedData = [...data.firstPackages, ...data.secondPackages];

      // Extract the keys to include in the CSV
      const keysToInclude = Object.keys(combinedData[0]).filter(
        key => key !== "Id" && key !== "DeliveryId"
      );

      // Convert the combined data to CSV format
      const csvContent =
      "data:text/csv;charset=utf-8," +
      [
        "Plan 1",
        keysToInclude.join(","),
        ...data.firstPackages.map(row =>
          keysToInclude.map(key => {
            if (["X", "Y", "Z"].includes(key)) {
              return String(row[key] * 100);
            }
            return String(row[key]);
          }).join(",")
        ),
        "",
        "Plan 2",
        keysToInclude.join(","),
        ...data.secondPackages.map(row =>
          keysToInclude.map(key => {
            if (["X", "Y", "Z"].includes(key)) {
              return String(row[key] * 100);
            }
            return String(row[key]);
          }).join(",")
        )
      ].join("\n");

      // Create a temporary link element
      const link = document.createElement("a");
      link.href = encodeURI(csvContent);
      link.download = "Solution.csv";

      // Simulate a click on the link to trigger the download
      link.click();
    };

    const showSolution1 = () => {
      if (!solution1Visible) {
        if (solution2Visible) {
          hideSolution2();
        }
        isGround = true;
        createGround();
        isGround = false;
        for (let i = 0; i < numOfBoxes1; i++) {
          boxWidth = boxesWidthFirst[i];
          boxHeight = boxesHeightFirst[i];
          boxDepth = boxesDepthFirst[i];
          
          const positionX = boxWidth/2 + xPositionsFirst[i] - groundWidth/2;
          const positionY = boxHeight/2 + yPositionsFirst[i];
          const positionZ = boxDepth/2 + zPositionsFirst[i] - groundDepth/2;
          const position = new THREE.Vector3(positionX, positionY, positionZ);

          const originalPositionX = xPositionsFirstOriginal[i]; 
          const originalPositionY = yPositionsFirstOriginal[i];
          const originalPositionZ = zPositionsFirstOriginal[i];
          const originalPosition = new THREE.Vector3(originalPositionX, originalPositionY, originalPositionZ);
          
          const id = boxesIdFirst[i];
          const order = boxesOrderFirst[i];

          const box = new Box({
            Id: id, 
            width: boxWidth,
            height: boxHeight,
            depth: boxDepth,
            position: position,
            originalPosition: originalPosition,
            order: order
          });
          box.castShadow = true;
          scene.add(box);
          boxes.push(box);
        }
        solution1Visible = true;
        if (!list1Visible) {
          if(list2Visible) {
            hideList2();
          }
          createList();
          list1Visible = true;
        }
      }
    };

    const hideSolution1 = () => {
      for (let i = 0; i < numOfBoxes1; i++) {
        const box = boxes[i];
        scene.remove(box);
      }
      boxes.splice(0, numOfBoxes1);
      solution1Visible = false;
      // console.log('scene.children:', scene.children);
    };

    const showSolution2 = () => {
      if (!solution2Visible) {
        if (solution1Visible) {
          hideSolution1();
        }
        isGround = true;
        createGround();
        isGround = false;
        for (let i = 0; i < numOfBoxes2; i++) {
          boxWidth = boxesWidthSecond[i];
          boxHeight = boxesHeightSecond[i];
          boxDepth = boxesDepthSecond[i];
          
          const positionX = boxWidth/2 + xPositionsSecond[i] - groundWidth/2;
          const positionY = boxHeight/2 + yPositionsSecond[i];
          const positionZ = boxDepth/2 + zPositionsSecond[i] - groundDepth/2;
          const position = new THREE.Vector3(positionX, positionY, positionZ);
          
          const originalPositionX = xPositionsSecondOriginal[i]; 
          const originalPositionY = yPositionsSecondOriginal[i];
          const originalPositionZ = zPositionsSecondOriginal[i];
          const originalPosition = new THREE.Vector3(originalPositionX, originalPositionY, originalPositionZ);

          const id = boxesIdSecond[i];
          const order = boxesOrderSecond[i];

          const box = new Box({
            Id: id,
            width: boxWidth,
            height: boxHeight,
            depth: boxDepth,
            position: position,
            originalPosition: originalPosition,
            order: order
          });
          box.castShadow = true;
          scene.add(box);
          boxes.push(box);
        }
        solution2Visible = true;
        if (!list2Visible) {
          if(list1Visible) {
            hideList1();
          }
          createList();
          list2Visible = true;
        }
      }
    };

    const hideSolution2 = () => {
      for (let i = 0; i < numOfBoxes2; i++) {
        const box = boxes[i];
        scene.remove(box);
      }
      boxes.splice(0, numOfBoxes2);
      solution2Visible = false;
    };

    // const light = new THREE.DirectionalLight(0xffffffff, 1);
    // light.position.set(3, 3, 3);
    // light.castShadow = true;
    // scene.add(light);

    const ambientLight = new THREE.AmbientLight(0xffffffff, 1);
    scene.add(ambientLight);

    // camera.position.x = -5;
    // camera.position.y = 5;
    camera.position.z = 20;
    
    window.addEventListener('mousemove', onMouseMove, false);
    window.addEventListener('click', onClick);
    
    animate();

    
    function createGround() {
      ground = new Box({
        id: null,
        width: groundWidth,
        height: groundHeight,
        depth: groundDepth,
        position: new THREE.Vector3(0, 0, 0),
        order: null
      });
  
      ground.receiveShadow = true;
      scene.add(ground);
    }

    function getRandomColor() {
      if (!isGround) {
        const baseHue = 200; // Adjust the base hue value to control the shade
        const saturation = Math.floor(Math.random() * 50) + 50; // Random saturation between 50 and 100
        const lightness = Math.floor(Math.random() * 30) + 35; // Random lightness between 35 and 65
        const color = `hsl(${baseHue}, ${saturation}%, ${lightness}%)`;
        return color;
      } else {
        // white
        return '#FFFFFF';
      }
    }

    // Check if there is a collision between box1 and box2
    function boxCollision({ box1, box2 }) {
      // Collision detection with x, y, z axes
      const xCollision = box1.right > box2.left && box1.left < box2.right;
      const yCollision = box1.top > box2.bottom && box1.bottom < box2.top;
      const zCollision = box1.back > box2.front && box1.front < box2.back;
      return xCollision && yCollision && zCollision;
    }

    function hoverBoxes() {
      raycaster.setFromCamera(mouse, camera);
      const intersects = raycaster.intersectObjects(scene.children);
      for (let i = 0; i < boxes.length; i++) {
        const box = boxes[i];
        if (box.material && box.material.color) {
          // if there is a selected box already - don't change color
          if (!selectedBox.current || box !== selectedBox.current) { 
            if (!box.originalColor) {
              box.originalColor = box.material.color.clone(); // Store the original color
            }
            if (intersects.some((intersect) => intersect.object === box)) {
              // Calculate a darker shade of the original color
              const originalColor = box.originalColor;
              const darkerColor = originalColor.clone().multiplyScalar(0.8); 
              box.material.color.copy(darkerColor);
            } else {
              box.material.color.copy(box.originalColor); // Reset to the original color
            }
          }
        }
      }
    }
    
    function animate() {
      controls.update();

      for (let i = 0; i < boxes.length; i++) {
        const box = boxes[i];
        box.update(ground);
      }

      hoverBoxes();

      renderer.render(scene, camera);
      window.requestAnimationFrame(animate);
    }

    function onMouseMove(event) {
      const canPlaceMaterial = new THREE.MeshStandardMaterial({ color: 0x00ff00 });
      const cannotPlaceMaterial = new THREE.MeshStandardMaterial({ color: 0xff0000 });
      event.preventDefault();
      // calculate mouse position in normalized device coordinates
      // (-1 to +1) for both components
      mouse.x = (event.clientX / window.innerWidth) * 2 - 1;
      mouse.y = -(event.clientY / window.innerHeight) * 2 + 1;
      raycaster.setFromCamera(mouse, camera);
      
      // Move the selected box with the mouse
      if (selectedBox.current) {
        const intersects = raycaster.intersectObjects(scene.children);
    
        if (intersects.length > 0) {
          const intersectionPoint = intersects[0].point;
    
          // Calculate the allowed boundary for moving the box
          const minX = ground.left + selectedBox.current.width / 2;
          const maxX = ground.right - selectedBox.current.width / 2;
          const minY = ground.top + selectedBox.current.height / 2;
          const maxY = ground.top + containerHeight - selectedBox.current.height / 2;
          const minZ = ground.front + selectedBox.current.depth / 2;
          const maxZ = ground.back - selectedBox.current.depth / 2;
    
          intersectionPoint.x = Math.max(Math.min(intersectionPoint.x, maxX), minX);
          intersectionPoint.y = Math.max(Math.min(intersectionPoint.y, maxY), minY);
          intersectionPoint.z = Math.max(Math.min(intersectionPoint.z, maxZ), minZ);

          // Check if the intersection point is within the container's boundaries
          if (
            intersectionPoint.x >= minX &&
            intersectionPoint.x <= maxX &&
            intersectionPoint.y >= minY &&
            intersectionPoint.y <= maxY &&
            intersectionPoint.z >= minZ &&
            intersectionPoint.z <= maxZ
          ) {
            // Check if there is a collision between the selected box and other boxes
            const hasCollision = boxes.some((box) => {
              return box !== selectedBox.current && boxCollision({ box1: selectedBox.current, box2: box });
            });
            if (hasCollision) {
              selectedBox.current.material = cannotPlaceMaterial; // Set the cannot-place material
            } else {
              selectedBox.current.material = canPlaceMaterial; // Set the can-place material
            }
            selectedBox.current.position.copy(intersectionPoint);
          } 
        } else {
          selectedBox.current.material = cannotPlaceMaterial; // Set the cannot-place material
        }
      }
    }

    function checkBoxBelow(box) {
      for (let i = 0; i < boxes.length; i++) {
        const otherBox = boxes[i];
        if (otherBox !== box) {
          // Check if the other box is below the given box
          if (otherBox.top <= box.bottom &&
            otherBox.right > box.left &&
            otherBox.left < box.right &&
            otherBox.back > box.front &&
            otherBox.front < box.back) {
            return true;
          }
        }
      }
      return false;
    }
    
    

    function onClick() {
      raycaster.setFromCamera(mouse, camera);
      const intersects = raycaster.intersectObjects(scene.children);
      if (intersects.length > 0) {
        if (selectedBox.current) {
          const intersectSelectedBox = raycaster.intersectObject(selectedBox.current);
          if (intersectSelectedBox.length > 0) {
            const intersectionPoint = intersectSelectedBox[0].point;
            // Calculate the allowed boundary for placing the box
            const minX = ground.left + selectedBox.current.width / 2;
            const maxX = ground.right - selectedBox.current.width / 2;
            const minY = ground.top + selectedBox.current.height / 2;
            const maxY = ground.top + containerHeight - selectedBox.current.height / 2;
            const minZ = ground.front + selectedBox.current.depth / 2;
            const maxZ = ground.back - selectedBox.current.depth / 2;

            intersectionPoint.x = Math.max(Math.min(intersectionPoint.x, maxX), minX);
            intersectionPoint.y = Math.max(Math.min(intersectionPoint.y, maxY), minY);
            intersectionPoint.z = Math.max(Math.min(intersectionPoint.z, maxZ), minZ);

            // Check if the intersection point is within the container's boundaries
            if (
              intersectionPoint.x >= minX &&
              intersectionPoint.x <= maxX &&
              intersectionPoint.y >= minY && 
              intersectionPoint.y <= maxY &&
              intersectionPoint.z >= minZ &&
              intersectionPoint.z <= maxZ) 
            {
              // Check if there is a collision between the selected box and other boxes
              const hasCollision = boxes.some((box) => {
                return box !== selectedBox.current && boxCollision({ box1: selectedBox.current, box2: box });
              });
              
              if (!hasCollision) {
                selectedBox.current.material = originalBoxMaterial; // Reset the material
                selectedBox.current.position.copy(intersectionPoint);
    
                // Check if the box is floating in the air
                if (checkBoxBelow(selectedBox.current)) {
                  // Find the box below the selected box
                  const boxBelow = boxes.find((otherBox) => 
                    otherBox.top <= selectedBox.current.bottom &&
                    otherBox.right > selectedBox.current.left &&
                    otherBox.left < selectedBox.current.right &&
                    otherBox.back > selectedBox.current.front &&
                    otherBox.front < selectedBox.current.back
                  );
                  selectedBox.current.position.y = boxBelow.top + selectedBox.current.height / 2; // Place the box on top of the box below it
                } else {
                  selectedBox.current.position.y = ground.top + selectedBox.current.height / 2; // Place the box on the ground
                }
                selectedBox.current = null; // Deselect the box
                const cancelButton = document.getElementById("cancel-button");
                if (cancelButton) {
                  cancelButton.remove(); // Remove the cancel button from the DOM
                }
              }
            }
          }
        } else if (intersects[0].object !== ground) {
          selectedBox.current = intersects[0].object;
          // Store the original position of the box
          originalPos.current = selectedBox.current.position.clone();
          displayBoxInfo(intersects[0].object);
          // Call the function to show the cancel button
          renderCancelButton();
        }
      }
    }

    function createList() {
      // Create a div element with the class name "box-list"
      const boxListDiv = document.createElement("div");
      boxListDiv.className = "box-list";
      boxListDiv.style.position = "absolute";
      boxListDiv.style.color = "white";
      boxListDiv.style.top = "50px";
      boxListDiv.style.left = "50px";
      ref.current.appendChild(boxListDiv);

      // Create an h1 element
      const h1 = document.createElement("h1");
      h1.innerHTML = "Packages:";
      boxListDiv.appendChild(h1);

      // Create a line break element
      const lineBreak = document.createElement("br");
      boxListDiv.appendChild(lineBreak);

      // Create a ul element
      const ul = document.createElement("ul");
      ul.style.position = "absolute";
      ul.style.overflowY = "auto"; 
      ul.style.maxHeight = "400px"; 
      ul.style.direction = "rtl"; 

      // Render the list of boxes
      boxes.forEach((box, index) => {
        const boxNumber = index + 1;

        // Create a button element for box
        const boxButton = document.createElement("button");
        boxButton.innerHTML = `Box ${boxNumber}`;
        boxButton.style.alignItems = "left";
        boxButton.style.backgroundColor = "orangered";
        boxButton.style.borderRadius = "5px 5px 5px 5px";
        boxButton.style.marginBottom = "10px"; 
        boxButton.addEventListener("click", () => {
          displayBoxInfo(box);
        });
        ul.appendChild(boxButton);
      });

      // Append the ul element to the box list div
      boxListDiv.appendChild(ul);
    }

    function displayBoxInfo(box) {
      // Create a div to hold the box information
      const boxInfoDiv = document.createElement("div");
      boxInfoDiv.style.position = "absolute";
      boxInfoDiv.style.top = "100px";
      boxInfoDiv.style.left = "200px";
      boxInfoDiv.style.backgroundColor = "white";
      boxInfoDiv.style.padding = "10px";
      boxInfoDiv.style.fontSize = "20px";
      boxInfoDiv.style.borderRadius = "5px 5px 5px 5px";

      // Create a line for each box property
      const properties = ["order", "width", "depth", "height", "x", "y", "z"];
      properties.forEach((property) => {
        const p = document.createElement("p");
        if(["order"].includes(property)) {
          p.innerHTML = `Order: ${box[property]}`;
        } else if (["width"].includes(property)) {
          // Multiply width by 100
          p.innerHTML = `Width: ${box[property] * 100}`;
        } else if (["depth"].includes(property)) {
          // Multiply depth by 100
          p.innerHTML = `Depth: ${box[property] * 100}`;
        } else if (["height"].includes(property)) {
          // Multiply height by 100
          p.innerHTML = `Height: ${box[property] * 100}`;
        } else if(["x"].includes(property)) {
          p.innerHTML = `X: ${box.originalPosition.x}`;
        } else if(["y"].includes(property)) {
          p.innerHTML = `Y: ${box.originalPosition.y}`;
        } else {
          p.innerHTML = `Z: ${box.originalPosition.z}`;
        }
        boxInfoDiv.appendChild(p);
      });

      // Add the box info div to the document body
      document.body.appendChild(boxInfoDiv);
    }

    function hideList1() {
      document.querySelector('.box-list h1').remove();
      // Select all button elements within the ul element
      const buttonElements = document.querySelectorAll('.box-list ul button');
    
      // Remove each li element
      buttonElements.forEach((button) => {
        button.remove();
      });
    
      list1Visible = false;
    }

    function hideList2() {
      document.querySelector('.box-list h1').remove();
      // Select all li elements within the ul element
      const buttonElements = document.querySelectorAll('.box-list ul button');
    
      // Remove each li element
      buttonElements.forEach((button) => {
        button.remove();
      });
    
      list2Visible = false;
    }

    // Create a div element for the export button
    const exportContainer = document.createElement("div");
    exportContainer.style.position = "absolute";
    exportContainer.style.bottom = "75px"; 
    exportContainer.style.right = "100px"; 
    ref.current.appendChild(exportContainer);

    // Create the export button and append it to the export container
    const exportButton = document.createElement("button");
    exportButton.className = "export-button";
    exportButton.innerHTML = "Export arrangements";
    exportButton.style.backgroundColor = "orangered";
    exportButton.style.borderRadius = "5px 5px 5px 5px";
    exportButton.style.padding = "10px";
    exportButton.addEventListener("click", handleExportSolution);
    exportContainer.appendChild(exportButton);

    // Create a div element for the solution1 button
    const solution1Container = document.createElement("div");
    solution1Container.style.position = "absolute";
    solution1Container.style.top = "50px"; 
    solution1Container.style.left = "480px"; 
    ref.current.appendChild(solution1Container);

    // Create the solution1 button and append it to the solution1 container
    const solution1Button = document.createElement("button");
    solution1Button.className = "solution1-button";
    solution1Button.innerHTML = "Arrangement plan 1";
    solution1Button.style.backgroundColor = "orangered";
    solution1Button.style.borderRadius = "5px 5px 5px 5px";
    solution1Button.style.padding = "10px";
    solution1Button.addEventListener("click", showSolution1);
    solution1Container.appendChild(solution1Button);

    // Create a div element for the solution2 button
    const solution2Container = document.createElement("div");
    solution2Container.style.position = "absolute";
    solution2Container.style.top = "50px"; 
    solution2Container.style.right = "480px"; 
    ref.current.appendChild(solution2Container);

    // Create the solution2 button and append it to the solution2 container
    const solution2Button = document.createElement("button");
    solution2Button.className = "solution2-button";
    solution2Button.innerHTML = "Arrangement plan 2";
    solution2Button.style.backgroundColor = "orangered";
    solution2Button.style.borderRadius = "5px 5px 5px 5px";
    solution2Button.style.padding = "10px";
    solution2Button.addEventListener("click", showSolution2);
    solution2Container.appendChild(solution2Button);
  
    return () => {
      
    };
    
  });

  return (
    <div className="visualization-container">
      <div className="visualization" ref={ref}></div>
      {renderCancelButton()}
    </div>
  );
};

export default Visualization;