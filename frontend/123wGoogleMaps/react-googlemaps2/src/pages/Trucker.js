import React from "react";
import { GoogleMap, useLoadScript, Marker } from "@react-google-maps/api";
import { Navibar } from "../components/Frame.js";
import PolledComponent from "../components/PolledComponent.js";

import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';

import { useEffect, useState, useRef } from 'react';

import logo from '../components/123loadboard.png';

import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
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
   useEffect(() => {
      fetch('https://jsonplaceholder.typicode.com/posts?_limit=10')
         .then((response) => response.json())
         .then((data) => {
            console.log(data);
            set2Posts(data);
         })
         .catch((err) => {
            console.log(err.message);
         });
   }, []);

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
        {/* <PolledComponent></PolledComponent> */}
        {/* <div className="posts-container" > */}
        <div class="flex-container">
          {/* <Container>
            <Row> */}
              {posts2.map((post) => {
                return (
                    <div key={post.id} class="flex-item">
                      <Col>
                        <Card style={{ width: '18rem' }}>
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
