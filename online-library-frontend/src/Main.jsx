import './css/Main.css';
import Search from './Search';
import ImageCarousel from './ImageCarousel';
import BookCarousel from './books/BookCarousel';

import React from 'react'

export default function Main() {
  return (
    <main>
        <Search />
        <ImageCarousel  />
        <BookCarousel title='Пушкин' titleUrl='/authors/aleksandr-pushkin' collectionUrl='http://localhost:7140/api/authors/aleksandr-pushkin' />
        <BookCarousel title='Мистика' titleUrl='/genres/mysticism' collectionUrl='http://localhost:7140/api/genres/mysticism' />
        <BookCarousel title='Гоголь' titleUrl='/authors/nikolay-gogol' collectionUrl='http://localhost:7140/api/authors/nikolay-gogol' />
    </main>
  )
}