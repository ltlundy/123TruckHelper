import React, { useState } from 'react';
import DataTable from 'react-data-table-component';

const columns = [
  {
    name: 'Truck ID',
    selector: (row) => row.truckId,
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
    name: 'Driver Current Location (Latitude, Longitude)',
    selector: (row) => row.currentLocation,
  },
  {
    name: 'Driver Distance to Load Origin',
    selector: (row) => row.distToStart,
  },
];

const data = [
    {
        truckId: 1,
        revenue: 10000,
        profit: 8000,
        origLat: 'OriginLat 1',
        origLon: 'OriginLong1',
        destLat: 'DestinationLat1',
        destLon: 'DesinationLong1',
        tripDist: '100 miles',
        currLat: 'Current Location 1',
        currLon: 0,
        distToStart: '20 miles',
    },
    {
        truckId: 2,
        revenue: 12000,
        profit: 9500,
        origLat: 'Origin 2',
        origLon: 'origin 2',
        destLat: 'Destination 2',
        destLong: 'origin 2',
        tripDist: '150 miles',
        currLat: 'Current Location 2',
        currLon: 0,
        distToStart: '15 miles',
    }, 
    {
        truckId: 3,
        revenue: 10000,
        profit: 8000,
        "origLat": 0,
        "origLon": 0,
        "destLat": 0,
        "destLon": 0,
        tripDist: '100 miles',
        currLat: 'Current Location 1',
        currLon: 0,
        distToStart: '20 miles',
    },
    {
        truckId: 4,
        revenue: 12000,
        profit: 9500,
        "origLat": 0,
        "origLon": 0,
        "destLat": 0,
        "destLon": 0,
        tripDist: '150 miles',
        currLat: 'Current Location 2',
        currLon: 0,
        distToStart: '15 miles',
    }, 
    {
        truckId: 5,
        revenue: 10000,
        profit: 8000,
        "origLat": 0,
        "origLon": 0,
        "destLat": 0,
        "destLon": 0,
        tripDist: '100 miles',
        currLat: 'Current Location 1',
        currLon: 0,
        distToStart: '20 miles',
    },
    {
        truckId: 6,
        revenue: 12000,
        profit: 9500,
        "origLat": 0,
        "origLon": 0,
        "destLat": 0,
        "destLon": 0,
        tripDist: '150 miles',
        currLat: 'Current Location 2',
        currLon: 0,
        distToStart: '15 miles',
    }, 
    {
        truckId: 7,
        revenue: 10000,
        profit: 8000,
        "origLat": 0,
        "origLon": 0,
        "destLat": 0,
        "destLon": 0,
        tripDist: '100 miles',
        currLat: 'Current Location 1',
        currLon: 0,
        distToStart: '20 miles',
    },
    {
        truckId: 8,
        revenue: 12000,
        profit: 9500,
        "origLat": 0,
        "origLon": 0,
        "destLat": 0,
        "destLon": 0,
        tripDist: '150 miles',
        currLat: 'Current Location 2',
        currLon: 0,
        distToStart: '15 miles',
    }, 
    {
        truckId: 9,
        revenue: 10000,
        profit: 8000,
        "origLat": 0,
        "origLon": 0,
        "destLat": 0,
        "destLon": 0,
        tripDist: '100 miles',
        currLat: 'Current Location 1',
        currLon: 0,
        distToStart: '20 miles',
    },
    {
        truckId: 10,
        revenue: 12000,
        profit: 9500,
        "origLat": 0,
        "origLon": 0,
        "destLat": 0,
        "destLon": 0,
        tripDist: '150 miles',
        currLat: 'Current Location 2',
        currLon: 0,
        distToStart: '15 miles',
    }, 
    {
        truckId: 11,
        revenue: 10000,
        profit: 8000,
        "origLat": 0,
        "origLon": 0,
        "destLat": 0,
        "destLon": 0,
        tripDist: '100 miles',
        currLat: 'Current Location 1',
        currLon: 0,
        distToStart: '20 miles',
    },
    {
        truckId: 12,
        revenue: 12000,
        profit: 9500,
        "origLat": 0,
        "origLon": 0,
        "destLat": 0,
        "destLon": 0,
        tripDist: '150 miles',
        currLat: 'Current Location 2',
        currLon: 0,
        distToStart: '15 miles',
    }
];

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
    filteredData[i].currentLocation = filteredData[i].currLat + ', ' + filteredData[i].currLon;

  }
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
      />
    </div>
  );
};

export default MyDataTable;
