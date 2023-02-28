import React, { useState, useEffect } from "react";

// Importing the Bootstrap 5 CSS
import "bootstrap/dist/css/bootstrap.min.css";

// let sandwiches = [
//   {
//     id: 1,
//     name: "Grilled Cheese Sandwich",
//     description: "A buttered and grill-toasted bread filled with your choice of cheese.",
//     price: 3.99,
//   },
//   {
//     id: 2,
//     name: "Sloppy Joe Sandwich",
//     description: "Ground beef, onion, garlic and bell pepper in a seasoned tomato sauce, served on a hamburger bun.",
//     price: 5.99,
//   },
// ];

// component to render the Sandwich object - listens to changes from input and runs the update() function, if any text field changes
const Sandwich = ({ sandwich }) => {
  const [data, setData] = useState(sandwich);
  const [dirty, setDirty] = useState(false);

  function update(value, fieldName, obj) {
    setData({ ...obj, [fieldName]: value });
    setDirty(true);
  }

  function onSave() {
    setDirty(false);
    // make rest call
  }

  return (
    <React.Fragment>
      <div className="row mt-3 mb-2">
        <div className="col-md-6">
          <div className="input-group input-group-lg mb-2">
            <input type="text" className="form-control" aria-label="input field" onChange={(evt) => update(evt.target.value, "name", data)} value={data.name} />
          </div>
          <div className="input-group input-group-md mb-2">
            <input type="text" className="form-control" aria-label="input field" onChange={(evt) => update(evt.target.value, "description", data)} value={data.description} />
          </div>
          <div className="input-group input-group-sm mb-2">
            <input type="text" className="form-control" aria-label="input field" onChange={(evt) => update(evt.target.value, "price", data)} value={data.price} />
          </div>
          {dirty ? (
            <div className="input-group input-group-sm mb-2">
              <button className="btn btn-primary" onClick={onSave}>
                Save
              </button>
            </div>
          ) : null}
        </div>
      </div>
    </React.Fragment>
  );
};

// renders the initial data -  a list of sandwiches
const Main = () => {
  const [sandwiches, setSandwiches] = useState([]);
  useEffect(() => {
    fetchData();
  }, []);

  function fetchData() {
    fetch("https://localhost:7182/sandwiches")
      .then((response) => response.json())
      .then((data) => setSandwiches(data));
  }

  const data = sandwiches.map((sandwich) => <Sandwich sandwich={sandwich} />);

  return <React.Fragment>{sandwiches.length === 0 ? <div>No sandwiches</div> : <div>{data}</div>}</React.Fragment>;
};

export default Main;
