﻿using System;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Play;
using Domain.Playlist;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

/*
 * Commando klasse. Den klasse styrer udpakningen af handlingen 'Remove Playlist'.
 * Den sender det udpakkede objekt til sin receiver IPlaylistService og returnerer et tomt TransferObj.
 */

namespace SocketsT1_T2.Tier2.Commands
{
    public class RemovePlaylistCommand:ICommand

    {
        private IPlayListService playListService;
        private TransferObj requestObj;
        

        public RemovePlaylistCommand(TransferObj requestObj)
        {
            playListService = ServicesFactory.GetPlayListService();
            this.requestObj = requestObj;
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                Playlist playlist = JsonElementConverter.ElementToObject<Playlist>(requestObj.Arg);
                await playListService.DeletePlayListAsync(playlist);
                return await ServerResponse.PrepareTransferObjectNoValueAsync();
            }
            catch (Exception e)
            {
                return await ServerResponse.PrepareTransferObjectWithExceptionAsync(e);
            }
            

        }
    }
}
