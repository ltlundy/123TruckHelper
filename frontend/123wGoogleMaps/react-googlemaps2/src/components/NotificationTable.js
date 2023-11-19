import React, { useEffect, useState } from "react";
import DataTable from "react-data-table-component";

export default function NotificationTable()
{
    const columns= [
        {
            name: "Truck ID", 
            selector:(row)=> row.truckId
        },
        {
            name:"Profit",
            selector:(row)=> row.revenue,
        },
        {
            name:"Net Profit",
            selector:(row)=>row.proft,
        },
        {
            name:"Load Origin",
            selector:(row)=>row.origLat,
        },
        {
            name:"Load Destination",
            selector:(row)=>row.destLat,
        },
        {
            name:"Trip Distance",
            selector:(row)=>row.tripDist,
        },
        {
            name:"Driver Current Location",
            selector:(row)=>row.currLat,
        },
        {
            name:"Driver Distance to Origin",
            selector:(row)=>row.distToStart,
        }

    ];
    const [data, setData]= useState([]);
    const [search, SetSearch]= useState('');
    const [filter, setFilter]= useState([]);

    const getProduct=async()=>{
    try{
        // const req= await fetch("https://fakestoreapi.com/products");
        // const res= await req.json();
        
        // for (let i=0; i<res.length; i++) { // use this to put the lat, long
        //     res[i].distance = res[i].title + ', ' + res[i].id;
        // }
        // res = [ 

        // ];
        const res = [
            {
                "truckId": 1,
                "revenue": 0,
                "proft": 0,
                "origLat": 0,
                "origLon": 0,
                "destLat": 0,
                "destLon": 0,
                "tripDist": 0,
                "distToStart": 0, 
                "currLat": 0,
                "currLon": 0
            }, 
            {
                "truckId": 1,
                "revenue": 0,
                "proft": 0,
                "origLat": 0,
                "origLon": 0,
                "destLat": 0,
                "destLon": 0,
                "tripDist": 0,
                "distToStart": 0, 
                "currLat": 0,
                "currLon": 0
            },
            {
                "truckId": 1,
                "revenue": 0,
                "proft": 0,
                "origLat": 0,
                "origLon": 0,
                "destLat": 0,
                "destLon": 0,
                "tripDist": 0,
                "distToStart": 0, 
                "currLat": 0,
                "currLon": 0
            }
        ];
        console.log(res);
        setData(res);
        setFilter(res);
        console.log(data);
        console.log(filter);
    } catch(error){
       console.log(error);
    }
    }
    useEffect(()=>{
        getProduct();
    }, []);

    useEffect(()=>{
        const result= data.filter((item)=>{
         return item.truckId.toLowerCase().match(search.toLocaleLowerCase());
        });
        setFilter(result);
    },[search]);
   
   const tableHeaderstyle={
    headCells:{
        style:{
            fontWeight:"bold",
            fontSize:"14px",
            backgroundColor:"#ccc"

        },
    },
   }

    return(
        <React.Fragment>
            <h1 style={{textAlign: "center"}}>Notifications</h1>
            <DataTable 
            customStyles={ tableHeaderstyle}
            columns={columns}
            data={filter}
            pagination
            fixedHeader
            selectableRowsHighlight
            highlightOnHover
            subHeader
             subHeaderComponent={
                <input type="text"
                className="w-25 form-control"
                placeholder="Search..."
                value={ search}
                onChange={(e)=>SetSearch(e.target.value)}
                
                />
             }
             subHeaderAlign="right"
            
            />
        </React.Fragment>
    );
}