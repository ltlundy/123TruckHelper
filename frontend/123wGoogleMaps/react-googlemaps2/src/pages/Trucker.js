import React from "react";
import { GoogleMap, useLoadScript, Marker } from "@react-google-maps/api";
import { Navibar } from "../components/Frame.js";

import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';

import { useEffect, useState, useRef } from 'react';
import { usePageVisibility } from '../components/usePageVisibility';

const libraries = ["places"];
const mapContainerStyle = {
  width: "100vw",
  height: "70vh",
};
const center = {
  lat: 7.2905715, // default latitude
  lng: 80.6337262, // default longitude
};

export function PolledComponent() {
    const isPageVisible = usePageVisibility();
    const timerIdRef = useRef(null);
    const [isPollingEnabled, setIsPollingEnabled] = useState(true);

    // const [timesPolled, setTimesPolled] = useState(0);
  
    useEffect(() => {
      const pollingCallback = () => {
        // Your polling logic here
        console.log('Polling...');
        
        // Simulating an API failure in the polling callback
        const shouldFail = Math.random() < 0.2; // Simulate 20% chance of API failure
  
        if (shouldFail) {
          setIsPollingEnabled(false);
          console.log('Polling failed. Stopped polling.');
        }
      };
  
      const startPolling = () => {
        pollingCallback(); // To immediately start fetching data
        // Polling every 30 seconds
        timerIdRef.current = setInterval(pollingCallback, 5000);
      };
  
      const stopPolling = () => {
        clearInterval(timerIdRef.current);
        console.log('setIsPollingEnabled2 = ' + isPollingEnabled);
      };
  
      if (isPageVisible && isPollingEnabled) {
        startPolling();
      } else {
        stopPolling();
      }
  
      return () => {
        stopPolling();
        // setTimesPolled(6);
        // console.log('timesPolled = ' + timesPolled);
      };
    }, [isPageVisible, isPollingEnabled]);
  
    return (
      <div>
        <img src="https://i.imgur.com/QIrZWGIs.jpg" alt="Alan L. Hart" />;
      </div>
    );
  }

const Trucker = () => {
  const { isLoaded, loadError } = useLoadScript({
    googleMapsApiKey: process.env.REACT_APP_GOOGLE_MAPS_KEY,
    libraries,
  });

  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  if (loadError) {
    return <div>Error loading maps</div>;
  }

  if (!isLoaded) {
    return <div>Loading maps</div>;
  }

  return (
    <div>
      <Navibar />
      <div>
        <GoogleMap
          mapContainerStyle={mapContainerStyle}
          zoom={10}
          center={center}
        >
          <Marker position={center} />
        </GoogleMap>
      </div>
      <hr></hr>
      <Button variant="primary" onClick={handleShow}>
        Launch demo modal
      </Button>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Modal heading</Modal.Title>
        </Modal.Header>
        <Modal.Body>Woohoo, you are reading this text in a modal!</Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick={handleClose}>
            Save Changes
          </Button>
        </Modal.Footer>
      </Modal>
      <div id="testGround">
        <PolledComponent></PolledComponent>
      </div>
    </div>
  );
};

export default Trucker;
