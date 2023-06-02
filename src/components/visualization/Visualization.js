import React, { useEffect, useRef } from 'react';
import './Visualization.css';
import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls';

const Visualization = () => {
  const canvasRef = useRef(null);

  useEffect(() => {
    const CONTAINER_HEIGHT = 5;
      
    var selectedBox = null;
    var boxes = [];

    const boxWidth = 1;
    const boxHeight = 1;
    const boxDepth = 1;
    const spacing = 0;

    const groundWidth = 10;
    const groundHeight = 0.1;
    const groundDepth = 5;
    const maxBoxesWidth = Math.floor(groundWidth / (boxWidth + spacing)) - 3;
    const maxBoxesHeight = Math.floor(CONTAINER_HEIGHT / (boxHeight + spacing)) - 2;
    const maxBoxesDepth = Math.floor(groundDepth / (boxDepth + spacing)) - 2;

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
    document.body.appendChild(renderer.domElement);

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
      
      update(ground) {
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

    for (let i = 0; i < maxBoxesWidth; i++) {
      for (let j = 0; j < maxBoxesHeight; j++) {
        for (let k = 0; k < maxBoxesDepth; k++) {
          const positionX = boxWidth/2 + (boxWidth + spacing) * i - groundWidth/2;
          const positionY = groundHeight/2 + boxHeight/2 + (boxHeight + spacing) * j;
          const positionZ = (boxDepth + spacing) * k;
          const position = new THREE.Vector3(positionX, positionY, positionZ);
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
      }
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
      const xCollision = box1.right >= box2.left && box1.left <= box2.right;
      const yCollision = box1.top >= box2.bottom && box1.bottom <= box2.top;
      const zCollision = box1.back >= box2.front && box1.front <= box2.back;
      return xCollision && yCollision && zCollision;
    }

    // Check if there is a collision between box1 and some other box
    function hasCollision(box1) {
      return boxes.some((box2) => {
        box2 !== box1 && boxCollision({ box1: box1, box2: box2 });
      });
    }

    function hoverBoxes() {
      raycaster.setFromCamera(mouse, camera);
      const intersects = raycaster.intersectObjects(scene.children);

      // Reset transparency for all boxes
      for (let i = 0; i < boxes.length; i++) {
        const box = boxes[i];
        if (box.material) {
          box.material.transparent = false;
          box.material.opacity = box == selectedBox ? 0.6 : 1.0;
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
      // calculate mouse position in normalized device coordinates
      // (-1 to +1) for both components
      mouse.x = (event.clientX / window.innerWidth) * 2 - 1;
      mouse.y = -(event.clientY / window.innerHeight) * 2 + 1;

      // Move the selected box with the mouse
      if (selectedBox) {
        raycaster.setFromCamera(mouse, camera);
        const intersects = raycaster.intersectObjects(scene.children);

        if (intersects.length > 0) {
          const intersectionPoint = intersects[0].point;
          // Check if the new position is within the container's boundaries
          const minX = ground.left + selectedBox.width / 2;
          const maxX = ground.right - selectedBox.width / 2;
          const minY = ground.top + selectedBox.height / 2;
          const maxY = ground.top + CONTAINER_HEIGHT - selectedBox.height / 2;
          const minZ = ground.front + selectedBox.depth / 2;
          const maxZ = ground.back - selectedBox.depth / 2;
          
          intersectionPoint.x = Math.max(Math.min(intersectionPoint.x, maxX), minX);
          intersectionPoint.y = Math.max(Math.min(intersectionPoint.y, maxY), minY);
          intersectionPoint.z = Math.max(Math.min(intersectionPoint.z, maxZ), minZ);
          
          // console.log('intersectionPoint.x:', intersectionPoint.x);
          // console.log('intersectionPoint.y:', intersectionPoint.y);
          // console.log('intersectionPoint.z:', intersectionPoint.z);
          
          selectedBox.position.copy(intersectionPoint);
        }
      }
    }

    function onClick(event) {
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
            const maxY = ground.top + CONTAINER_HEIGHT - selectedBox.height / 2;
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
  }, []);

  return <canvas ref={canvasRef}/>;
};

export default Visualization;