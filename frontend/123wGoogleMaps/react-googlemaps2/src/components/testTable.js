import React, { useState } from 'react';
import DataTable from 'react-data-table-component';

const columns = [
  {
    name: "Load ID", 
    selector:(row)=> row.loadId,
  },
  {
    name: 'Truck ID',
    selector: (row) => row.truckId,
  },
  {
    name: 'Notification Timestamp',
    selector: (row) => row.timestamp,
  },
  {
    name: 'Revenue',
    selector: (row) => row.revenue,
  },
  {
    name: 'Estimated Profit',
    selector: (row) => row.profit,
  },
  {
    name: 'Load Origin (Latitude, Longitude)',
    selector: (row) => row.origin,
  },
  {
    name: 'Load Destination (Latitude, Longitude)',
    selector: (row) => row.destination,
  },
  {
    name: 'Trip Distance',
    selector: (row) => row.tripDist,
  },
  {
    name: 'Driver Distance to Load Origin',
    selector: (row) => row.distToStart,
  }
];

const req= await fetch("http://localhost:5016/all");
const data= await req.json();

const MyDataTable = () => {
  const [searchText, setSearchText] = useState('');

  const handleSearch = (e) => {
    const text = e.target.value;
    setSearchText(text);
  };

  const filteredData = data.filter((item) =>
    item.truckId.toString().toLowerCase().includes(searchText.toLowerCase())
  );
  for (let i=0; i<filteredData.length; i++) {
    filteredData[i].origin = filteredData[i].origLat + ', ' + filteredData[i].origLon;
    filteredData[i].destination = filteredData[i].destLat + ', ' + filteredData[i].destLon;
  }
  //const filteredData = data;
  const tableHeaderstyle={
    headCells:{
        style:{
            fontWeight:"bold",
            fontSize:"18px",
            backgroundColor:"#7e7e7e", 
            minHeight: '100px',
            overflowWrap: 'break-word'
        },
    },
    header: {
        style: {
            fontWeight: "bold",
            fontSize: "34px"
        },
    },
    cells: {
        style: {
            fontSize:"16px",
            backgroundColor:"#d3d3d3"
        },
    },
   }

  return (
    <div className="my-data-table-container">
      <div className="my-data-table-header">
        <input
          type="text"
          placeholder="Search by Truck ID..."
          value={searchText}
          onChange={handleSearch}
          className="search-bar"
        />
      </div>
      <DataTable
        customStyles={ tableHeaderstyle}
        title="Notifications"
        columns={columns}
        data={filteredData}
        pagination
        keyField='truckId'
      />
    </div>
  );
};

export default MyDataTable;
