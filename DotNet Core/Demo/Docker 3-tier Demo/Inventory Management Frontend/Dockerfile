# Use the lightweight Alpine version of Nginx
FROM nginx:alpine

# Remove the default Nginx configuration file
RUN rm /etc/nginx/conf.d/default.conf

# Copy your custom Nginx configuration
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copy your HTML files to the Nginx serving directory
COPY index.html /usr/share/nginx/html/index.html
COPY dashboard.html /usr/share/nginx/html/dashboard.html

# Expose port 80 (Standard web traffic port)
EXPOSE 80

# Start Nginx in the foreground so the container keeps running
CMD ["nginx", "-g", "daemon off;"]