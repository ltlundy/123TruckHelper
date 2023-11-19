import React from "react";
import { GoogleMap, useLoadScript, Marker } from "@react-google-maps/api";
import { Navibar } from "../components/Frame.js";
// import { MyComponent } from "../components/PolledComponent.js";
import { usePageVisibility } from "../components/usePageVisibility.js";

import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import CardGroup from 'react-bootstrap/CardGroup';

import { useEffect, useState, useRef } from 'react';

import logo from '../components/123loadboard.png';

const libraries = ["places"];
const mapContainerStyle = {
  width: "100vw",
  height: "60vh",
};
const center = {
  lat: 45.50478, // default latitude
  lng: -73.57706, // default longitude
};

const Trucker = () => {
  const { isLoaded, loadError } = useLoadScript({
    googleMapsApiKey: process.env.REACT_APP_GOOGLE_MAPS_KEY,
    libraries,
  });

  const [posts2, set2Posts] = useState([]);
  const isPageVisible = usePageVisibility();
  const timerIdRef = useRef(null);

  useEffect(() => {
    const pollingCallback = () => {
      // fetch('https://jsonplaceholder.typicode.com/posts?_limit=10')
      fetch('http://localhost:8000/datum')
         .then((response) => response.json())
         .then((data) => {
            console.log(data); // DEBUGGING
            set2Posts(data);
         })
         .catch((err) => {
            console.log(err.message);
         });
         console.log('Polling...');
    }
    const startPolling = () => {
      pollingCallback(); // To immediately start fetching data
      // Polling every 30 seconds CHANGE REFRESH RATE
      timerIdRef.current = setInterval(pollingCallback, 1000);
    };

    const stopPolling = () => {
      clearInterval(timerIdRef.current);
    };
// only polls if webpage is currently viewed! Doesn't fetch if ex. window is minimized!
    if (isPageVisible) {
      startPolling();
    } else {
      stopPolling();
    }

    return () => {
      stopPolling();
    };
  }, [isPageVisible]);

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
          zoom={17}
          center={center}
        >
          <Marker position={center} />
        </GoogleMap>
      </div>
      <hr></hr>
      <div id="testGround">
        {/* <MyComponent /> */}
        <div className="flex-container">
          <CardGroup>
            {posts2.map((post) => {
              return (
                <div key={post.id} className="flex-item">
                  <Card style={{ width: '18rem', padding: '15px', height: '25rem'}}>
                    <Card.Img variant="top" src={logo} style={{padding: '5px'}}/>
                    <Card.Body>
                      <Card.Title>Profit = {post.profit}</Card.Title>
                      <Card.Subtitle className="mb-2 text-muted">Trip Distance : {post.tripDistance}km</Card.Subtitle>
                      <Card.Text>
                        Start of route: {post.startLocation} <br />
                        End of route: {post.endLocation} <br />
                        Distance to Start: {post.distanceToStart}km
                      </Card.Text>
                      <Button variant="primary">Go somewhere</Button>
                    </Card.Body>
                    <Card.Footer>Message ID = {post.id}</Card.Footer>
                  </Card>
                </div>
              );
            })}
          </CardGroup>
        </div>
      </div>
    </div>
  );
};

export default Trucker;
