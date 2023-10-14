//import logo from './logo.svg';
import './App.css';

//import { BrowserRouter, Route, NavLink,  } from 'react-router-dom';
import Layout from './components/shared/shared/Layout';
import AddProduct from './pages/AddProduct';
import AllProduct from './pages/AllProduct';
import { Route, Routes } from "react-router-dom";
import UpdateProduct from './pages/UpdateProduct';
import Login from './pages/Login';
import { AuthContextProvider } from './components/shared/shared/AuthContext';
import Home from "./pages/Home";
import ProtectedRoute from "./components/shared/shared/ProtectedRoute";

function App() {
  return (
    <AuthContextProvider>
    <Layout>
    <h1>Hello People</h1>
    
    <Routes>
    <Route path="/" element={<AllProduct/>}></Route>
 
    <Route
      path="/login"
      element={
     <ProtectedRoute accessBy="non-authenticated">
       <Login />
     </ProtectedRoute>
   }
 ></Route>
        
        
        <Route
              path="/Home"
              element={
                <ProtectedRoute accessBy="">
                  <Home/>
                </ProtectedRoute>
              }
            ></Route>
            </Routes>
      <Routes>
        <Route path="/AddProduct" element={<AddProduct />} />
      </Routes>
      <Routes>
        <Route path="/UpdateProduct" element={<UpdateProduct />}/>
      </Routes>
    </Layout>
    </AuthContextProvider>
  );
}
export default App;
