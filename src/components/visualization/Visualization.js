import React, { useEffect, useRef } from 'react';
import './Visualization.css';
import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls';

const Visualization = () => {
  const ref = useRef(null);

  useEffect(() => {
    const data = {"id": 1, 
    "container": {
      "height": 600, 
      "width": 800, 
      "depth": 1600, 
      "cost": 0}, 
    "firstPackages": [
      {"id": 6, 
      "width": 200, 
      "depth": 800, 
      "height": 200, 
      "order": 5, 
      "x": 0, 
      "y": 0, 
      "z": 0, 
      "deliveryId": 1}, 
      {"id": 23, "width": 200, "depth": 800, "height": 200, "order": 22, "x": 0, "y": 0, "z": 200, "deliveryId": 1}, {"id": 31, "width": 200, "depth": 800, "height": 200, "order": 30, "x": 0, "y": 0, "z": 400, "deliveryId": 1}, {"id": 29, "width": 200, "depth": 800, "height": 200, "order": 28, "x": 0, "y": 200, "z": 0, "deliveryId": 1}, {"id": 30, "width": 200, "depth": 800, "height": 200, "order": 29, "x": 0, "y": 200, "z": 200, "deliveryId": 1}, {"id": 43, "width": 200, "depth": 800, "height": 200, "order": 42, "x": 0, "y": 200, "z": 400, "deliveryId": 1}, {"id": 36, "width": 200, "depth": 800, "height": 200, "order": 35, "x": 0, "y": 400, "z": 0, "deliveryId": 1}, {"id": 37, "width": 200, "depth": 800, "height": 200, "order": 36, "x": 0, "y": 400, "z": 200, "deliveryId": 1}, {"id": 10, "width": 200, "depth": 200, "height": 600, "order": 9, "x": 0, "y": 600, "z": 0, "deliveryId": 1}, {"id": 9, "width": 200, "depth": 200, "height": 600, "order": 8, "x": 200, "y": 600, "z": 0, "deliveryId": 1}, {"id": 11, "width": 200, "depth": 200, "height": 600, "order": 10, "x": 400, "y": 600, "z": 0, "deliveryId": 1}, {"id": 44, "width": 200, "depth": 200, "height": 600, "order": 43, "x": 600, "y": 600, "z": 0, "deliveryId": 1}, {"id": 33, "width": 200, "depth": 1200, "height": 100, "order": 32, "x": 0, "y": 400, "z": 400, "deliveryId": 1}, {"id": 38, "width": 200, "depth": 200, "height": 600, "order": 37, "x": 800, "y": 0, "z": 0, "deliveryId": 1}, {"id": 2, "width": 200, "depth": 200, "height": 400, "order": 1, "x": 800, "y": 200, "z": 0, "deliveryId": 1}, {"id": 3, "width": 400, "depth": 200, "height": 200, "order": 2, "x": 800, "y": 400, "z": 0, "deliveryId": 1}, {"id": 28, "width": 400, "depth": 200, "height": 200, "order": 27, "x": 800, "y": 400, "z": 200, "deliveryId": 1}, {"id": 18, "width": 800, "depth": 100, "height": 200, "order": 17, "x": 1000, "y": 0, "z": 0, "deliveryId": 1}, {"id": 22, "width": 400, "depth": 200, "height": 200, "order": 21, "x": 1000, "y": 0, "z": 200, "deliveryId": 1}, {"id": 24, "width": 400, "depth": 200, "height": 200, "order": 23, "x": 1000, "y": 0, "z": 400, "deliveryId": 1}, {"id": 27, "width": 400, "depth": 200, "height": 200, "order": 26, "x": 1000, "y": 400, "z": 200, "deliveryId": 1}, {"id": 32, "width": 800, "depth": 100, "height": 200, "order": 31, "x": 1100, "y": 0, "z": 0, "deliveryId": 1}, {"id": 39, "width": 400, "depth": 200, "height": 200, "order": 38, "x": 1200, "y": 0, "z": 0, "deliveryId": 1}, {"id": 35, "width": 400, "depth": 200, "height": 200, "order": 34, "x": 1200, "y": 0, "z": 200, "deliveryId": 1}, {"id": 40, "width": 400, "depth": 200, "height": 200, "order": 39, "x": 1200, "y": 0, "z": 400, "deliveryId": 1}, {"id": 41, "width": 400, "depth": 200, "height": 200, "order": 40, "x": 1200, "y": 400, "z": 0, "deliveryId": 1}, {"id": 42, "width": 400, "depth": 200, "height": 200, "order": 41, "x": 1200, "y": 400, "z": 200, "deliveryId": 1}, {"id": 5, "width": 200, "depth": 300, "height": 200, "order": 4, "x": 800, "y": 600, "z": 400, "deliveryId": 1}, {"id": 4, "width": 200, "depth": 300, "height": 200, "order": 3, "x": 1100, "y": 600, "z": 400, "deliveryId": 1}, {"id": 7, "width": 200, "depth": 100, "height": 600, "order": 6, "x": 1400, "y": 0, "z": 0, "deliveryId": 1}, {"id": 12, "width": 200, "depth": 100, "height": 600, "order": 11, "x": 1400, "y": 200, "z": 0, "deliveryId": 1}, {"id": 8, "width": 200, "depth": 100, "height": 600, "order": 7, "x": 1400, "y": 400, "z": 0, "deliveryId": 1}, {"id": 13, "width": 200, "depth": 100, "height": 600, "order": 12, "x": 1400, "y": 600, "z": 0, "deliveryId": 1}, {"id": 34, "width": 200, "depth": 100, "height": 600, "order": 33, "x": 1500, "y": 0, "z": 0, "deliveryId": 1}, {"id": 19, "width": 200, "depth": 200, "height": 200, "order": 18, "x": 800, "y": 200, "z": 400, "deliveryId": 1}, {"id": 16, "width": 200, "depth": 200, "height": 200, "order": 15, "x": 1200, "y": 400, "z": 400, "deliveryId": 1}
    ], 
    "secondPackages": [
      {"id": 6, 
      "width": 200, 
      "depth": 800, 
      "height": 200, 
      "order": 5, 
      "x": 0, 
      "y": 0, 
      "z": 0, 
      "deliveryId": 1}, 
      {"id": 23, "width": 200, "depth": 800, "height": 200, "order": 22, "x": 0, "y": 0, "z": 200, "deliveryId": 1}, {"id": 31, "width": 200, "depth": 800, "height": 200, "order": 30, "x": 0, "y": 0, "z": 400, "deliveryId": 1}, {"id": 9, "width": 200, "depth": 200, "height": 600, "order": 8, "x": 200, "y": 600, "z": 0, "deliveryId": 1}, {"id": 38, "width": 200, "depth": 200, "height": 600, "order": 37, "x": 800, "y": 0, "z": 0, "deliveryId": 1}, {"id": 18, "width": 800, "depth": 100, "height": 200, "order": 17, "x": 1000, "y": 0, "z": 0, "deliveryId": 1}, {"id": 22, "width": 400, "depth": 200, "height": 200, "order": 21, "x": 1000, "y": 0, "z": 200, "deliveryId": 1}, {"id": 24, "width": 400, "depth": 200, "height": 200, "order": 23, "x": 1000, "y": 0, "z": 400, "deliveryId": 1}, {"id": 32, "width": 800, "depth": 100, "height": 200, "order": 31, "x": 1100, "y": 0, "z": 0, "deliveryId": 1}, {"id": 39, "width": 400, "depth": 200, "height": 200, "order": 38, "x": 1200, "y": 0, "z": 0, "deliveryId": 1}, {"id": 35, "width": 400, "depth": 200, "height": 200, "order": 34, "x": 1200, "y": 0, "z": 200, "deliveryId": 1}, {"id": 40, "width": 400, "depth": 200, "height": 200, "order": 39, "x": 1200, "y": 0, "z": 400, "deliveryId": 1}, {"id": 7, "width": 200, "depth": 100, "height": 600, "order": 6, "x": 1400, "y": 0, "z": 0, "deliveryId": 1}, {"id": 12, "width": 200, "depth": 100, "height": 600, "order": 11, "x": 1400, "y": 200, "z": 0, "deliveryId": 1}, {"id": 8, "width": 200, "depth": 100, "height": 600, "order": 7, "x": 1400, "y": 400, "z": 0, "deliveryId": 1}, {"id": 13, "width": 200, "depth": 100, "height": 600, "order": 12, "x": 1400, "y": 600, "z": 0, "deliveryId": 1}, {"id": 34, "width": 200, "depth": 100, "height": 600, "order": 33, "x": 1500, "y": 0, "z": 0, "deliveryId": 1}, {"id": 4, "width": 200, "depth": 300, "height": 200, "order": 3, "x": 0, "y": 200, "z": 0, "deliveryId": 1}, {"id": 44, "width": 200, "depth": 600, "height": 200, "order": 43, "x": 0, "y": 200, "z": 200, "deliveryId": 1}, {"id": 16, "width": 200, "depth": 200, "height": 200, "order": 15, "x": 0, "y": 200, "z": 400, "deliveryId": 1}, {"id": 43, "width": 200, "depth": 800, "height": 200, "order": 42, "x": 0, "y": 400, "z": 0, "deliveryId": 1}, {"id": 36, "width": 200, "depth": 800, "height": 200, "order": 35, "x": 0, "y": 400, "z": 200, "deliveryId": 1}, {"id": 37, "width": 200, "depth": 800, "height": 200, "order": 36, "x": 0, "y": 400, "z": 400, "deliveryId": 1}, {"id": 19, "width": 200, "depth": 200, "height": 200, "order": 18, "x": 0, "y": 600, "z": 0, "deliveryId": 1}, {"id": 5, "width": 200, "depth": 300, "height": 200, "order": 4, "x": 200, "y": 200, "z": 400, "deliveryId": 1}, {"id": 29, "width": 200, "depth": 800, "height": 200, "order": 28, "x": 600, "y": 600, "z": 200, "deliveryId": 1}, {"id": 30, "width": 200, "depth": 800, "height": 200, "order": 29, "x": 600, "y": 600, "z": 400, "deliveryId": 1}, {"id": 3, "width": 200, "depth": 400, "height": 200, "order": 2, "x": 300, "y": 200, "z": 0, "deliveryId": 1}, {"id": 27, "width": 200, "depth": 400, "height": 200, "order": 26, "x": 500, "y": 200, "z": 400, "deliveryId": 1}, {"id": 2, "width": 200, "depth": 400, "height": 200, "order": 1, "x": 600, "y": 200, "z": 200, "deliveryId": 1}, {"id": 28, "width": 200, "depth": 400, "height": 200, "order": 27, "x": 600, "y": 600, "z": 0, "deliveryId": 1}, {"id": 41, "width": 200, "depth": 400, "height": 200, "order": 40, "x": 800, "y": 400, "z": 200, "deliveryId": 1}, {"id": 42, "width": 200, "depth": 400, "height": 200, "order": 41, "x": 800, "y": 400, "z": 400, "deliveryId": 1}, {"id": 14, "width": 100, "depth": 200, "height": 200, "order": 13, "x": 0, "y": 600, "z": 200, "deliveryId": 1}, {"id": 15, "width": 200, "depth": 300, "height": 200, "order": 14, "x": 700, "y": 200, "z": 0, "deliveryId": 1}, {"id": 25, "width": 200, "depth": 200, "height": 200, "order": 24, "x": 0, "y": 600, "z": 400, "deliveryId": 1}, {"id": 26, "width": 200, "depth": 200, "height": 200, "order": 25, "x": 800, "y": 400, "z": 0, "deliveryId": 1}
    ]
    };

    // first solution:

    const boxesWidthFirst = [];
    const boxesHeightFirst = [];
    const boxesDepthFirst = [];

    data.firstPackages.forEach(function(box) {
      boxesWidthFirst.push(box.width / 100);
      boxesHeightFirst.push(box.height / 100);
      boxesDepthFirst.push(box.depth / 100);
    });

    const xPositionsFirst = [];
    const yPositionsFirst = [];
    const zPositionsFirst = [];

    data.firstPackages.forEach(function(box) {
      if (box.x !== 0) {
        box.x /= 100;
      }
      zPositionsFirst.push(box.x);
      if (box.y !== 0) {
        box.y /= 100;
      }
      xPositionsFirst.push(box.y);
      if (box.z !== 0) {
        box.z /= 100;
      }
      yPositionsFirst.push(box.z);
    });

    // second solution:

    const boxesWidthSecond = [];
    const boxesHeightSecond = [];
    const boxesDepthSecond = [];

    data.secondPackages.forEach(function(box) {
      boxesWidthSecond.push(box.width / 100);
      boxesHeightSecond.push(box.height / 100);
      boxesDepthSecond.push(box.depth / 100);
    });

    const xPositionsSecond = [];
    const yPositionsSecond = [];
    const zPositionsSecond = [];

    data.secondPackages.forEach(function(box) {
      if (box.x !== 0) {
        box.x /= 100;
      }
      zPositionsSecond.push(box.x);
      if (box.y !== 0) {
        box.y /= 100;
      }
      xPositionsSecond.push(box.y);
      if (box.z !== 0) {
        box.z /= 100;
      }
      yPositionsSecond.push(box.z);
    });
    
    // console.log('xPositionsFirst:', xPositionsFirst);
    // console.log('yPositionsFirst', yPositionsFirst);
    // console.log('zPositionsFirst', zPositionsFirst);
    
    const containerHeight = data.container.height / 100;
      
    var selectedBox = null;
    var boxes = [];

    var boxWidth = 0;
    var boxHeight = 0;
    var boxDepth = 0;
    // const spacing = 0;

    const groundWidth = data.container.width / 100;
    const groundHeight = 0;
    const groundDepth = data.container.depth / 100;
    // const maxBoxesWidth = Math.floor(groundWidth / (boxWidth + spacing)) - 3;
    // const maxBoxesHeight = Math.floor(containerHeight / (boxHeight + spacing)) - 2;
    // const maxBoxesDepth = Math.floor(groundDepth / (boxDepth + spacing)) - 2;

    //const startX = -((maxBoxesWidth - 1) * (boxWidth + spacing)) / 2;
    //const startY = -((maxBoxesHeight - 1) * (boxHeight + spacing)) / 2;
    //const startZ = -((maxBoxesDepth - 1) * (boxDepth + spacing)) / 2;1

    var scene = new THREE.Scene();
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

    class Box extends THREE.Mesh {
      constructor({ width, height, depth, position }) {
        const color = getRandomColor();
        const material = new THREE.MeshStandardMaterial({ color: color });

        super(new THREE.BoxGeometry(width, height, depth), material);
        
        this.height = height;
        this.width = width;
        this.depth = depth;
        
        this.position.copy(position);
        // this.velocity = velocity;
        
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
        
        // this.position.x += this.velocity.x;
        // this.position.y += this.velocity.y;
        // this.position.z += this.velocity.z;

        // this.applyGravity(ground);
      }
        
      // applyGravity(ground) {

      //   // Collision detection with the ground
      //   if (boxCollision({ box1: this, box2: ground })) {
      //     this.velocity.y = 0;
      //     this.position.y = ground.top + this.height / 2;
      //   }

      // }

    }

    // Creating boxes and ground

    var numOfBoxes = xPositionsFirst.length;
    for (let i = 0; i < numOfBoxes; i++) {
      boxWidth = boxesWidthFirst[i];
      boxHeight = boxesHeightFirst[i];
      boxDepth = boxesDepthFirst[i];
      // const positionX = boxWidth/2 + (boxWidth + spacing) * i - groundWidth/2;
      // const positionY = groundHeight/2 + boxHeight/2 + (boxHeight + spacing) * i;
      // const positionZ = boxDepth/2 + (boxDepth + spacing) * i - groundDepth/2;
      const positionX = boxWidth/2 + xPositionsFirst[i] - groundWidth/2;
      const positionY = boxHeight/2 + yPositionsFirst[i];
      const positionZ = boxDepth/2 + zPositionsFirst[i] - groundDepth/2;
      const position = new THREE.Vector3(positionX, positionY, positionZ);
      console.log('position', i, ':', position);
      // const velocity = new THREE.Vector3(0, 0, 0); // Example velocity

      const box = new Box({
        width: boxWidth,
        height: boxHeight,
        depth: boxDepth,
        // velocity: velocity,
        position: position,
      });
      box.castShadow = true;
      scene.add(box);
      boxes.push(box);
    }

    const ground = new Box({
      width: groundWidth,
      height: groundHeight,
      depth: groundDepth,
      position: new THREE.Vector3(0, 0, 0),
    });

    ground.receiveShadow = true;
    scene.add(ground);

    // const light = new THREE.DirectionalLight(0xffffffff, 1);
    // light.position.set(3, 3, 3);
    // light.castShadow = true;
    // scene.add(light);

    const ambientLight = new THREE.AmbientLight(0xffffffff, 1);
    scene.add(ambientLight);

    // camera.position.x = -5;
    // camera.position.y = 5;
    camera.position.z = 13;
    
    window.addEventListener('mousemove', onMouseMove, false);
    window.addEventListener('click', onClick);
    
    animate();

    

    function getRandomColor() {
      const letters = '0123456789ABCDEF';
      let color = '#';
      for (let i = 0; i < 3; i++) {
        color += letters[Math.floor(Math.random() * 16)];
      }
      return color;
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

      // Reset transparency for all boxes
      for (let i = 0; i < boxes.length; i++) {
        const box = boxes[i];
        if (box.material) {
          box.material.transparent = false;
          box.material.opacity = box === selectedBox ? 0.6 : 1.0;
        }
      }
      // Set transparency for intersected boxes
      for (let i = 0; i < intersects.length; i++) {
        const object = intersects[i].object;
        if (boxes.includes(object) && object.material) {
          if (!selectedBox) { // if there is a selected box already - don't change opacity
            object.material.transparent = true;
            object.material.opacity = 0.5;
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
      event.preventDefault();
      // calculate mouse position in normalized device coordinates
      // (-1 to +1) for both components
      mouse.x = (event.clientX / window.innerWidth) * 2 - 1;
      mouse.y = -(event.clientY / window.innerHeight) * 2 + 1;
      raycaster.setFromCamera(mouse, camera);
      
      // Move the selected box with the mouse
      if (selectedBox) {
        const intersects = raycaster.intersectObjects(scene.children);
    
        if (intersects.length > 0) {
          const intersectionPoint = intersects[0].point;
    
          // Calculate the allowed boundary for moving the box
          const minX = ground.left + selectedBox.width / 2;
          const maxX = ground.right - selectedBox.width / 2;
          const minY = ground.top + selectedBox.height / 2;
          const maxY = ground.top + containerHeight - selectedBox.height / 2;
          const minZ = ground.front + selectedBox.depth / 2;
          const maxZ = ground.back - selectedBox.depth / 2;
    
          intersectionPoint.x = Math.max(Math.min(intersectionPoint.x, maxX), minX);
          intersectionPoint.y = Math.max(Math.min(intersectionPoint.y, maxY), minY);
          intersectionPoint.z = Math.max(Math.min(intersectionPoint.z, maxZ), minZ);
    
          // console.log('intersectionPoint.x:', intersectionPoint.x);
          // console.log('intersectionPoint.y:', intersectionPoint.y);
          // console.log('intersectionPoint.z:', intersectionPoint.z);

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
              return box !== selectedBox && boxCollision({ box1: selectedBox, box2: box });
            });
    
            // Move the selected box only if there is no collision or after a collision to a neighboring free place
            if (!hasCollision || !boxCollision({ box1: selectedBox, box2: intersects[0].object })) {
              selectedBox.position.copy(intersectionPoint);
            }
          }
        }
      }
    }
    
    function onClick() {
      raycaster.setFromCamera(mouse, camera);
      const intersects = raycaster.intersectObjects(scene.children);

      if (intersects.length > 0) {
        if (selectedBox) {
          const intersectSelectedBox = raycaster.intersectObject(selectedBox);

          if (intersectSelectedBox.length > 0) {
            const intersectionPoint = intersectSelectedBox[0].point;

            // console.log('scene.children:', scene.children);
            // console.log('Intersects:', intersects);
            // console.log('intersectSelectedBox:', intersectSelectedBox);
            // console.log('intersectSelectedBox[0]:', intersectSelectedBox[0]);
            // console.log('Intersection Point:', intersectionPoint);

            // Calculate the allowed boundary for placing the box
            const minX = ground.left + selectedBox.width / 2;
            const maxX = ground.right - selectedBox.width / 2;
            const minY = ground.top + selectedBox.height / 2;
            const maxY = ground.top + containerHeight - selectedBox.height / 2;
            const minZ = ground.front + selectedBox.depth / 2;
            const maxZ = ground.back - selectedBox.depth / 2;

            // console.log('Min X:', minX);
            // console.log('Max X:', maxX);
            // console.log('Min Y:', minY);
            // console.log('Max Y:', maxY);
            // console.log('Min Z:', minZ);
            // console.log('Max Z:', maxZ);

            intersectionPoint.x = Math.max(Math.min(intersectionPoint.x, maxX), minX);
            intersectionPoint.y = Math.max(Math.min(intersectionPoint.y, maxY), minY);
            intersectionPoint.z = Math.max(Math.min(intersectionPoint.z, maxZ), minZ);

            // Check if the intersection point is within the container's boundaries
            if (intersectionPoint.x >= minX && intersectionPoint.x <= maxX &&
            intersectionPoint.y >= minY && intersectionPoint.y <= maxY &&
            intersectionPoint.z >= minZ &&intersectionPoint.z <= maxZ) {
              // Check if there is a collision between the selected box and other boxes
              const hasCollision = boxes.some((box) => {
                return box !== selectedBox && boxCollision({ box1: selectedBox, box2: box });
              });
              
              if (!hasCollision) {
                selectedBox.position.copy(intersectionPoint);
                selectedBox = null; // Deselect the box after placing it
              }
            }
          }
        } else if (intersects[0].object !== ground) {
          selectedBox = intersects[0].object;
        }
      }
    }

  
    return () => {
      // Clean up Three.js scene here
    };
    
  }, []);

  return <div ref={ref}></div>;
};

export default Visualization;