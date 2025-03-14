import { useState } from 'react';

function ContactForm() {
  const [formData, setFormData] = useState({ name: '', email: '', message: '' });

  const handleChange = (event: { target: { name: any; value: any; }; }) => {
    setFormData({ ...formData, [event.target.name]: event.target.value });
  };

  const handleSubmit = (event: { preventDefault: () => void; }) => {
    event.preventDefault();
    // Handle form submission, e.g., send data to a backend or email service
    console.log(formData);
  };

  return (
    <form onSubmit={handleSubmit}>
      <label>
        Name:
        <input type="text" name="name" value={formData.name} onChange={handleChange} />
      </label>
      <br />
      <label>
        Email:
        <input type="email" name="email" value={formData.email} onChange={handleChange} />
      </label>
      <br />
      <label>
        Message:
        <textarea name="message" value={formData.message} onChange={handleChange} />
      </label>
      <br />
      <button type="submit">Submit</button>
    </form>
  );
}

export default ContactForm;