//import logo from './logo.svg';
import './App.css';
import { Home } from './Home';
import { Category } from './Category';
import { Product } from './Product';
import { BrowserRouter, Route, NavLink } from 'react-router-dom';

function App() {
  return (
    <BrowserRouter>
      <div className="App container">
        <h3 className="d-flex justify-content-center m-3">
          Sushi
        </h3>

        <nav className="navbar navbar-expand-sm bg-light navbar-dark">
          <ul className="navbar-nav">
            <li className="nav-item m-1">
              <NavLink className="btn btn-light btn-outline-primary" to="/home">
                Home
              </NavLink>
            </li>
            <li className="nav-item m-1">
              <NavLink className="btn btn-light btn-outline-primary" to="/category">
                Category
              </NavLink>
            </li>
            <li className="nav-item m-1">
              <NavLink className="btn btn-light btn-outline-primary" to="/product">
                Product
              </NavLink>
            </li>
          </ul>
        </nav>

        <switch>
          <Route path='/Home' component={Home}></Route>
          <Route path='/Category' component={Category}></Route>
          <Route path='/Product' component={Product}></Route>
        </switch>
      </div>
    </BrowserRouter>
  );
}
export default App;
