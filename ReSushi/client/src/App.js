import axios from 'axios';
import React, { useState, useEffect } from 'react';
import ProductList from './components/ProductList';
import ProductForm from './components/ProductForm';
import ProductDelete from './components/ProductDelete';
import Category from './components/Category';
import CategoryForm from './components/CategoryForm';

function App() {
  const [categories, setCategories] = useState([]);

  useEffect(() => {
    fetchCategories();
  }, []);

  const fetchCategories = async () => {
    try {
      const response = await axios.get('/api/Categories/GetAll');
      setCategories(response.data);
    } catch (error) {
      console.error('Error fetching categories:', error);
    }
  };

  const handleCategoryAdded = (newCategory) => {
    setCategories([...categories, newCategory]);
  };

  return (
    <div>
      <ProductList />
      <ProductForm />
      <ProductDelete productId={/укажите идентификатор товара/} />
      <Category categories={categories} />
      <CategoryForm onCategoryAdded={handleCategoryAdded} />
    </div>
  );
}

export default App;
