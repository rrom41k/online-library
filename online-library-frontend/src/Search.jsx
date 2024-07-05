import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './css/Search.css';

export default function Search() {
  const [searchTerm, setSearchTerm] = useState('');
  const navigate = useNavigate();

  const searchSubmit = (e) => {
    e.preventDefault();
    navigate(`/books?searchTerm=${searchTerm}`);
    window.location.reload();
  };

  return (
    <div className='search'>
      <form onSubmit={searchSubmit}>
        <input 
          type="text" 
          value={searchTerm} 
          onChange={(e) => setSearchTerm(e.target.value)} 
          placeholder="Введите название книги/автора/жанра"
        />
        <button type="submit">
          <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="white" className="bi bi-search" viewBox="0 0 16 16">
            <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001q.044.06.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1 1 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0"/>
          </svg>
        </button>
      </form>
    </div>
  );
}
