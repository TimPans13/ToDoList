import React, { useState, useEffect } from 'react';
import './App.css';
import { FaPlus, FaTrash, FaCopy, FaEdit, FaCheck } from 'react-icons/fa';
import axios from 'axios';

const App = () => {
  const [todos, setTodos] = useState([]);
  const [input, setInput] = useState('');
  const [editingTodo, setEditingTodo] = useState(null);
  const [editedText, setEditedText] = useState('');

  useEffect(() => {
    // Получаем данные с сервера при загрузке компонента
    axios.get('https://localhost:7045/api/v1/orders/Task-Gets')
      .then(response => {
        setTodos(response.data);
      })
      .catch(error => {
        console.error('Error fetching data:', error);
      });
  }, []); 

  const addTodo = () => {
    if (input.trim() !== '') {
      const newTodo = {
        text: input,
        completed: false,
      };

      // Отправляем POST запрос на сервер для добавления новой задачи
      axios.post('https://localhost:7045/api/v1/orders/Task-Add', newTodo)
        .then(response => {
          console.log("response.data= "+response.data)
          setTodos([...todos,response.data]);
          //setTodos([...todos, newTodo]);
          
        })
        .catch(error => {
          console.error('Error adding task:', error);
        });

      setInput('');
      
    }
  };

  const deleteTodo = (id) => {
    // Отправляем DELETE запрос на сервер для удаления задачи
    axios.delete(`https://localhost:7045/api/v1/orders/Task-Destroy/${id}`)
      .then(response => {
        setTodos(todos.filter(todo => todo.id !== id));
      })
      .catch(error => {
        console.error('Error deleting task:', error);
      });
  };

  const toggleTodo = (id) => {
    // Отправляем PUT запрос на сервер для изменения статуса задачи
    axios.put(`https://localhost:7045/api/v1/orders/Task-Toggle/${id}`)
      .then(response => {
        setTodos(todos.map(todo => (todo.id === id ? { ...todo, completed: !todo.completed } : todo)));
      })
      .catch(error => {
        console.error('Error toggling task:', error);
      });
  };

  const copyTodo = (id) => {
    // Отправляем POST запрос на сервер для копирования задачи
    axios.post(`https://localhost:7045/api/v1/orders/Task-Copy/${id}`)
      .then(response => {
        console.log("response.data= "+response.data)
        setTodos([...todos, response.data]);
      })
      .catch(error => {
        console.error('Error copying task:', error);
      });
  };

  const editTodo = (id) => {
    // Устанавливаем идентификатор редактируемой задачи в состояние
    setEditingTodo(id);
    // Находим текст редактируемой задачи и устанавливаем его в состояние editedText
    const todoToEdit = todos.find(todo => todo.id === id);
    if (todoToEdit) {
      setEditedText(todoToEdit.text);
    }
  };

  // const updateTodo = (id ) => {
  //   // Отправляем PUT запрос на сервер для обновления текста задачи
  //   // console.log("editedText type= "+{ text: editedText })
  //   // console.log("editedText = "+ { text: editedText })
  //   axios.put(`https://localhost:7045/api/v1/orders/Task-Update/${id}`, { text: editedText })
  //     .then(response => {
  //       // Обновляем текст задачи на клиенте после успешного обновления на сервере
  //       setTodos(todos.map(todo => (todo.id === id ? { ...todo, text: editedText } : todo)));
  //       // Завершаем редактирование, сбрасывая состояния
  //       setEditingTodo(null);
  //       setEditedText('');
  //     })
  //     .catch(error => {
  //       console.error('Error updating task:', error);
  //     });
  // };

  const updateTodo = (id) => {
    // Отправляем PUT запрос на сервер для обновления текста задачи
    axios.put(`https://localhost:7045/api/v1/orders/Task-Update/${id}`, `"${editedText}"`, {
      headers: {
        'Content-Type': 'application/json'
      }
    })
      .then(response => {
        // Обновляем текст задачи на клиенте после успешного обновления на сервере
        setTodos(todos.map(todo => (todo.id === id ? { ...todo, text: editedText } : todo)));
        // Завершаем редактирование, сбрасывая состояния
        setEditingTodo(null);
        setEditedText('');
      })
      .catch(error => {
        console.error('Error updating task:', error);
      });
  };
  
  
  



  return (
    <div className="container">
      <h1>Todo List</h1>
      <div className="todo-form">
        <input
          type="text"
          value={input}
          onChange={(e) => setInput(e.target.value)}
          className="todo-input"
          placeholder="Enter your task"
        />
        <FaPlus className="add-icon" onClick={addTodo} />
      </div>
      <div className="todo-list">
        {todos.map(todo => (
          <div className={`todo-item ${todo.completed ? 'completed' : ''}`} key={todo.id}>
            {editingTodo === todo.id ? (
              <div className="todo-item-edit">
                <input
                  type="text"
                  value={editedText}
                  onChange={(e) => setEditedText(e.target.value)}
                  className="edit-input"
                />
                <FaCheck className="edit-icon" onClick={() => updateTodo(todo.id)} />
              </div>
            ) : (
              <span onClick={() => toggleTodo(todo.id)}>{todo.text}</span>
            )}
            <div className="todo-item-buttons">
              <FaCopy className="action-icon" onClick={() => copyTodo(todo.id)} />
              <FaEdit className="action-icon" onClick={() => editTodo(todo.id)} />
              <FaTrash className="action-icon" onClick={() => deleteTodo(todo.id)} />
              <FaCheck className="action-icon" onClick={() => toggleTodo(todo.id)} />
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default App;
