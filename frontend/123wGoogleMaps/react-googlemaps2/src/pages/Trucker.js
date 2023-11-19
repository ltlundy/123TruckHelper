import React from "react";
import { GoogleMap, useLoadScript, Marker } from "@react-google-maps/api";
import { Navibar } from "../components/Frame.js";
// import { MyComponent } from "../components/PolledComponent.js";
import { usePageVisibility } from "../components/usePageVisibility.js";

import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';

import { useEffect, useState, useRef } from 'react';

import logo from '../components/123loadboard.png';

import Col from 'react-bootstrap/Col';

const libraries = ["places"];
const mapContainerStyle = {
  width: "100vw",
  height: "60vh",
};
const center = {
  lat: 7.2905715, // default latitude
  lng: 80.6337262, // default longitude
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
          zoom={10}
          center={center}
        >
          <Marker position={center} />
        </GoogleMap>
      </div>
      <hr></hr>
      <div id="testGround">
        {/* <MyComponent /> */}
        {/* <div className="posts-container" > */}
        <div className="flex-container">
          {/* <Container>
            <Row> */}
              {posts2.map((post) => {
                return (
                    <div key={post.id} className="flex-item">
                      <Col>
                        <Card style={{ width: '18rem', padding: '15px'}}>
                          <Card.Img variant="top" src={logo} />
                          <Card.Body>
                            <Card.Title>{post.title}</Card.Title>
                            <Card.Subtitle className="mb-2 text-muted">{post.id}</Card.Subtitle>
                            <Card.Text>
                              {post.body}
                            </Card.Text>
                            <Button variant="primary">Go somewhere</Button>
                          </Card.Body>
                        </Card>
                      </Col>
                    </div>
                );
              })}
            {/* </Row>
          </Container> */}
        </div>
      </div>
    </div>
  );
};

export default Trucker;
