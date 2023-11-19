import React from "react";
import { Navibar } from "../components/Frame.js";
import NotificationTable from "../components/NotificationTable.js";
import Product from "../components/testTable.js";

function Admin() {
  return (
    <div>
      <Navibar />
      <h1>Admin Page</h1>
      <Product />
    </div>
  );
}

export default Admin;
