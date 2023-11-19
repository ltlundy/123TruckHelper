import React, { useState } from 'react';
import DataTable from 'react-data-table-component';

const columns = [
  {
    name: 'Truck ID',
    selector: (row) => row.truckId,
  },
  {
    name: 'Profit',
    selector: (row) => row.revenue,
  },
  {
    name: 'Net Profit',
    selector: (row) => row.profit,
  },
  {
    name: 'Load Origin',
    selector: (row) => row.origLat,
  },
  {
    name: 'Load Destination',
    selector: (row) => row.destLat,
  },
  {
    name: 'Trip Distance',
    selector: (row) => row.tripDist,
  },
  {
    name: 'Driver Current Location',
    selector: (row) => row.currLat,
  },
  {
    name: 'Driver Distance to Origin',
    selector: (row) => row.distToStart,
  },
];

const data = [
    {
        truckId: 1,
        revenue: 10000,
        profit: 8000,
        origLat: 'Origin 1',
        destLat: 'Destination 1',
        tripDist: '100 miles',
        currLat: 'Current Location 1',
        distToStart: '20 miles',
    },
    {
        truckId: 2,
        revenue: 12000,
        profit: 9500,
        origLat: 'Origin 2',
        destLat: 'Destination 2',
        tripDist: '150 miles',
        currLat: 'Current Location 2',
        distToStart: '15 miles',
    }, 
    {
        truckId: 3,
        revenue: 10000,
        profit: 8000,
        origLat: 'Origin 1',
        destLat: 'Destination 1',
        tripDist: '100 miles',
        currLat: 'Current Location 1',
        distToStart: '20 miles',
    },
    {
        truckId: 4,
        revenue: 12000,
        profit: 9500,
        origLat: 'Origin 2',
        destLat: 'Destination 2',
        tripDist: '150 miles',
        currLat: 'Current Location 2',
        distToStart: '15 miles',
    }, 
    {
        truckId: 5,
        revenue: 10000,
        profit: 8000,
        origLat: 'Origin 1',
        destLat: 'Destination 1',
        tripDist: '100 miles',
        currLat: 'Current Location 1',
        distToStart: '20 miles',
    },
    {
        truckId: 6,
        revenue: 12000,
        profit: 9500,
        origLat: 'Origin 2',
        destLat: 'Destination 2',
        tripDist: '150 miles',
        currLat: 'Current Location 2',
        distToStart: '15 miles',
    }, 
    {
        truckId: 7,
        revenue: 10000,
        profit: 8000,
        origLat: 'Origin 1',
        destLat: 'Destination 1',
        tripDist: '100 miles',
        currLat: 'Current Location 1',
        distToStart: '20 miles',
    },
    {
        truckId: 8,
        revenue: 12000,
        profit: 9500,
        origLat: 'Origin 2',
        destLat: 'Destination 2',
        tripDist: '150 miles',
        currLat: 'Current Location 2',
        distToStart: '15 miles',
    }, 
    {
        truckId: 9,
        revenue: 10000,
        profit: 8000,
        origLat: 'Origin 1',
        destLat: 'Destination 1',
        tripDist: '100 miles',
        currLat: 'Current Location 1',
        distToStart: '20 miles',
    },
    {
        truckId: 10,
        revenue: 12000,
        profit: 9500,
        origLat: 'Origin 2',
        destLat: 'Destination 2',
        tripDist: '150 miles',
        currLat: 'Current Location 2',
        distToStart: '15 miles',
    }, 
    {
        truckId: 11,
        revenue: 10000,
        profit: 8000,
        origLat: 'Origin 1',
        destLat: 'Destination 1',
        tripDist: '100 miles',
        currLat: 'Current Location 1',
        distToStart: '20 miles',
    },
    {
        truckId: 12,
        revenue: 12000,
        profit: 9500,
        origLat: 'Origin 2',
        destLat: 'Destination 2',
        tripDist: '150 miles',
        currLat: 'Current Location 2',
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

  const tableHeaderstyle={
    headCells:{
        style:{
            fontWeight:"bold",
            fontSize:"14px",
            backgroundColor:"#ccc"

        },
    },
    title: {
        style: {
            fontWeight: "bold",
            fontSize: "24px"
        },
    },
    bodyCells: {
        style:{
            fontWeight:"bold",
            fontSize:"14px",
            backgroundColor:"#FFF"

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
