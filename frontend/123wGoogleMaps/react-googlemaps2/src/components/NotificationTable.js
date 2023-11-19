import React, { useEffect, useState } from "react";
import DataTable from "react-data-table-component";
export default function Product()
{
    const columns= [
        {
            name:"Sr.No",
            selector:(row)=>row.id,
        },
        {
            name:"Title",
            selector:(row)=>row.title,
        },
        {
            name:"Category",
            selector:(row)=>row.category,
        },
        {
            name:"Price",
            selector:(row)=>row.price,
        },
        {
            name:"Image",
            selector:(row)=><img  height ={70} width={80} src={ row.image}/>,
        },
        {
            name:"Action",
            cell:(row)=>(
                <button className="btn btn-danger" onClick={()=>handleDelete(row.id)}>Delete</button>
            )
        }

    ];
    const [data, setData]= useState([]);
    const [search, SetSearch]= useState('');
    const [filter, setFilter]= useState([]);

    const getProduct=async()=>{
    try{
        const req= await fetch("https://fakestoreapi.com/products");
        const res= await req.json();
        setData(res);
        setFilter(res);
    } catch(error){
       console.log(error);
    }
    }
    useEffect(()=>{
        getProduct();
    }, []);

    useEffect(()=>{
        const result= data.filter((item)=>{
         return item.title.toLowerCase().match(search.toLocaleLowerCase());
        });
        setFilter(result);
    },[search]);

   const handleDelete=(val)=>{
    const newdata= data.filter((item)=>item.id!==val);
    setFilter(newdata);
   }
   
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
            <h1>Product List</h1>
            <DataTable 
            customStyles={ tableHeaderstyle}
            columns={columns}
            data={filter}
            pagination
            selectableRows
            fixedHeader
            selectableRowsHighlight
            highlightOnHover
            actions={
                <button className="btn btn-success">Export Pdf</button>
            }
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