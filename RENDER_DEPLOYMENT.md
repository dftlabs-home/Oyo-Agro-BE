# Render Deployment Guide for OyoAgro API

This guide will help you deploy the OyoAgro API to Render's free tier.

## Prerequisites

1. A GitHub account (or GitLab/Bitbucket)
2. A Render account (sign up at https://render.com)
3. Your code pushed to a Git repository

## Step-by-Step Deployment

### Option 1: Deploy using render.yaml (Recommended)

1. **Push your code to GitHub**
   ```bash
   git add .
   git commit -m "Prepare for Render deployment"
   git push origin main
   ```

2. **Connect to Render**
   - Go to https://dashboard.render.com
   - Click "New +" and select "Blueprint"
   - Connect your GitHub repository
   - Render will automatically detect the `render.yaml` file

3. **Configure Environment Variables**
   - After the blueprint is created, go to your service settings
   - **IMPORTANT**: Update these environment variables:
     - `Database__DefaultConnection`: Replace `YOUR_EXISTING_DATABASE_CONNECTION_STRING` with your actual database connection string
     - `Jwt__Key`: Replace `CHANGE_THIS_TO_A_SECURE_RANDOM_STRING` with a secure random string (at least 32 characters)
       - Generate a key using:
         - PowerShell: `[Convert]::ToBase64String((1..32 | ForEach-Object { Get-Random -Minimum 0 -Maximum 256 }))`
         - Or use an online generator: https://www.grc.com/passwords.htm

4. **Deploy**
   - Render will automatically build and deploy your application
   - Your API will be available at: `https://your-service-name.onrender.com`

### Option 2: Manual Deployment

1. **Create Web Service**
   - Click "New +" → "Web Service"
   - Connect your GitHub repository
   - Configure:
     - **Name**: `oyoagro-api`
     - **Environment**: `Docker`
     - **Dockerfile Path**: `./Dockerfile`
     - **Docker Context**: `.`
     - **Plan**: Free

3. **Set Environment Variables**
   ```
   ASPNETCORE_ENVIRONMENT=Production
   Database__DBProvider=postgresql
   Database__DefaultConnection=<your-postgres-connection-string>
   Jwt__Key=<your-secure-jwt-key>
   Jwt__Issuer=OyoAgroApi
   Jwt__Audience=OyoAgroClient
   Jwt__DurationInMinutes=3
   ```

4. **Deploy**
   - Click "Create Web Service"
   - Render will build and deploy your application

## Environment Variables

The following environment variables need to be set:

| Variable | Description | Example |
|----------|-------------|---------|
| `ASPNETCORE_ENVIRONMENT` | Environment name | `Production` |
| `Database__DBProvider` | Database provider | `postgresql` |
| `Database__DefaultConnection` | Database connection string | Your existing database connection string |
| `Jwt__Key` | JWT secret key | Generate a secure random string |
| `Jwt__Issuer` | JWT issuer | `OyoAgroApi` |
| `Jwt__Audience` | JWT audience | `OyoAgroClient` |
| `Jwt__DurationInMinutes` | Token duration | `3` |

## Important Notes

1. **Free Tier Limitations**:
   - Services spin down after 15 minutes of inactivity
   - First request after spin-down may take 30-60 seconds

2. **Database Migrations**:
   - You may need to run migrations manually after deployment
   - Consider adding migration scripts to your deployment process

3. **HTTPS**:
   - Render provides HTTPS automatically
   - Your API will be accessible at `https://your-service.onrender.com`

4. **Swagger UI**:
   - Access Swagger at: `https://your-service.onrender.com/swagger`

## Troubleshooting

### Build Fails
- Check Dockerfile path is correct
- Ensure all project references are included
- Check build logs in Render dashboard

### Database Connection Issues
- Verify your external database connection string format
- Ensure SSL mode is set correctly (if required by your database provider)
- Check that your database is accessible from Render's servers (whitelist Render IPs if needed)
- Test the connection string locally before deploying

### Application Won't Start
- Check application logs in Render dashboard
- Verify PORT environment variable is being used
- Ensure all required environment variables are set

## Support

For Render-specific issues, check:
- Render Documentation: https://render.com/docs
- Render Community: https://community.render.com

