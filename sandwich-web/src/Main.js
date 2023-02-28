import React, { useState } from "react";

let sandwiches = [
  {
    id: 1,
    name: "Grilled Cheese Sandwich",
    description: "A buttered and grill-toasted bread filled with your choice of cheese.",
    price: 3.99,
  },
  {
    id: 2,
    name: "Sloppy Joe Sandwich",
    description: "Ground beef, onion, garlic and bell pepper in a seasoned tomato sauce, served on a hamburger bun.",
    price: 5.99,
  },
];

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
      <div>
        <h3>
          <input onChange={(evt) => update(evt.target.value, "name", data)} value={data.name} />
        </h3>
        <div>
          <input onChange={(evt) => update(evt.target.value, "description", data)} value={data.description} />
        </div>
        <div>
          <input onChange={(evt) => update(evt.target.value, "price", data)} value={data.price} />
        </div>
        {dirty ? (
          <div>
            <button onClick={onSave}>Save</button>
          </div>
        ) : null}
      </div>
    </React.Fragment>
  );
};

// renders the initial data -  a list of sandwiches
const Main = () => {
  const data = sandwiches.map((sandwich) => <Sandwich sandwich={sandwich} />);

  return <React.Fragment>{data}</React.Fragment>;
};

export default Main;
