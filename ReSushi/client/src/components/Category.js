import React, { useState, useEffect } from 'react';
import axios from 'axios';


const Category = ({Categories }) => {
  const [categories, setCategories] = useState([
    { id: 1, name: 'Category 1', description: 'Description 1' },
    { id: 2, name: 'Category 2', description: 'Description 2' },
    { id: 1, name: 'Category 3', description: 'Description 3' },
    { id: 2, name: 'Category 4', description: 'Description 4' },
  ]);
  const [selectedCategory, setSelectedCategory] = useState(null);

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

  const handleCategorySelect = (id) => {
    const selected = categories.find(category => category.id === id);
    setSelectedCategory(selected);
  };

  return (
    <div>
      <h2>Categories</h2>
      <ul>
        {categories.map(category => (
          <li key={category.id}>
            <button onClick={() => handleCategorySelect(category.id)}>
              {category.name}
            </button>
          </li>
        ))}
      </ul>
      {selectedCategory && (
        <div>
          <h3>Selected Category</h3>
          <p>ID: {selectedCategory.id}</p>
          <p>Name: {selectedCategory.name}</p>
          <p>Description: {selectedCategory.description}</p>
        </div>
      )}
    </div>
  );
};

export default Category;
