import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import ReactDOM from 'react-dom/client';
import AuthorsPage from './authors/AuthorsPage';
import AuthorPage from './authors/AuthorPage';
import BooksPage from './books/BooksPage';
import BookPage from './books/BookPage';
import GenresPage from './GenresPage';
import NotFound from './NotFound';
import Header from './Header';
import Main from './Main';
import React from 'react';
import './css/index.css';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <Header />
    <Router>
        <Routes>
          <Route path="/" element={<Main />} />
          
          <Route path="/authors" element={<AuthorsPage />} />
          <Route path='/authors/:slug' element={<AuthorPage />} />

          <Route path="/genres" element={<GenresPage />}/>

          <Route path="/books" element={<BooksPage />}/>
          <Route path="/books/:slug" element={<BookPage />}/>
          <Route path="*" element={<NotFound />} />
        </Routes>
    </Router>
  </React.StrictMode>
);