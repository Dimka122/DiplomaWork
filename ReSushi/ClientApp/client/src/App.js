//import logo from './logo.svg';
import './App.css';
import Layout from './components/shared/Layout';
import AllSupProduct from './pages/AllSupProduct';
//import AllSupProduct from './pages/AllSupProduct';
import AddSupProduct from './pages/AddSupProduct';
//import UpdateSupProduct from './pages/UpdateSupProduct';
import { Route, Routes } from "react-router-dom";

function App() {
  return (
    <Layout>
      <h1>Products</h1>
      <Routes>
        <Route path="/" element={<AllSupProduct />} />
      </Routes>
      <Routes>
        <Route path="/product-create" element={<AddSupProduct />} />
      </Routes>
      
    </Layout>
  );
}

export default App;
