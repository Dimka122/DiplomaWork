//import logo from './logo.svg';
import './App.css';

//import { BrowserRouter, Route, NavLink,  } from 'react-router-dom';
import Layout from './components/shared/Layout';
import AddProduct from './pages/AddProduct';
import AllProduct from './pages/AllProduct';
import { Route, Routes } from "react-router-dom";
import UpdateProduct from './pages/UpdateProduct';


function App() {
  return (
    <Layout>
    <h1>Hello People</h1>
    <Routes>
        <Route path="/" element={<AllProduct />} />
      </Routes>
      <Routes>
        <Route path="/AddProduct" element={<AddProduct />} />
      </Routes>
      <Routes>
        <Route
          path="/Products/id"
          element={<UpdateProduct />}
        />
      </Routes>
    </Layout>
  );
}
export default App;
