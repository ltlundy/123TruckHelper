import React from "react";
import { Navibar } from "../components/Frame.js";
import Table from "../components/testTable.js";
import Product from "../components/NotificationTable.js"
function Admin() {
  return (
    <div>
      <Navibar />
      <h1>Admin Page</h1>
      <Table />
    </div>
  );
}

export default Admin;
