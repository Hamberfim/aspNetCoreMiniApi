// import logo from './logo.svg';
import "./App.css";

import Main from "./Main.js";

function App() {
  return (
    <div className="App container-fluid">
      <div className="row">
        <div className="col-md-6">
          <h1 className="display-1 text-primary">
            <span className="text-left text-uppercase text-nowrap">The Sandwich'er</span>
          </h1>
        </div>
      </div>
      <Main />
    </div>
  );
}

export default App;
