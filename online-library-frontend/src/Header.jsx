import "./css/Header.css";

function Header() {
  return (
    <header>
      <div className="container">
        <div className="logo">
          <div className="logo_kubstu">
            <a href="https://kubstu.ru">
              <img src="http://localhost:3000/Logo KubStu.png" alt="KubStu" />
            </a>
          </div>
          <div className="logo_text">
            <a href="/">Библиотека политеха</a>
          </div>
        </div>

        {/* Навигация  */}

        <nav>
          <ul className="menu_items">
            <a href="/books">
              <li className="menu_item">Книги</li>
            </a>
            <a href="/authors">
              <li className="menu_item">Авторы</li>
            </a>
            <a href="/genres">
              <li className="menu_item">Жанры</li>
            </a>
          </ul>
        </nav>
      </div>
    </header>
  );
}

export default Header;
