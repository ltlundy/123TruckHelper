import React from 'react';
import LoginPage from "./pages/Login.js"; // Assuming the LoginPage component is in a separate file
import { Navibar } from "./components/Frame.js";

const App = () => {
  return (
    <div>
      <Navibar />
      <h1 style={{textAlign: "center"}}>Welcome to 123TruckHelper</h1>
      <LoginPage />
    </div>
  );
};

export default App;
