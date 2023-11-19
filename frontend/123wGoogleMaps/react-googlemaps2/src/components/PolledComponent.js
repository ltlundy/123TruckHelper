import React from "react";
import { useEffect, useState, useRef } from 'react';
import { usePageVisibility } from './usePageVisibility';


export default function PolledComponent() {
    const isPageVisible = usePageVisibility();
    const timerIdRef = useRef(null);
    const [isPollingEnabled, setIsPollingEnabled] = useState(true);

    const [posts, setPosts] = useState([])

    // const [timesPolled, setTimesPolled] = useState(0);
  
    useEffect(() => {
      const pollingCallback = () => {
        fetch("http://localhost:8000/posts").then(res => res.json())
        .then(data => {
            setPosts(data);
        });// Your polling logic here
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
        // console.log('setIsPollingEnabled2 = ' + isPollingEnabled);
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
        {posts.map(post => (
            <div key={post.id}>
                <h3>{post.title}</h3>
                <p> Authored by {post.author}</p>
                <hr/>
            </div>
        ))}
      </div>
    );
  }